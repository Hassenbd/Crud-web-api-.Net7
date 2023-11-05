using AutoMapper;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;

public class CharacterService : ICharactersService
{
    private static  List<Character> characters = new List<Character>
    {
        new Character(),
        new Character(){Id=1,Name="hassen"}
    };

    private readonly  IMapper autoMap;
    private readonly DataContext db;
    public CharacterService(IMapper autoMap , DataContext db )
    {
        this.autoMap=autoMap;
        this.db=db;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> addChracter(AddCharacterDto ch)
    {
        var  response=new ServiceResponse<List<GetCharacterDto>>();
        var character=new Character();
        character=autoMap.Map<Character>(ch);
        character.Id=characters.Max(ele=>ele.Id)+1;

        characters.Add(character);
        response.Data=characters.Select(ele=>autoMap.Map<GetCharacterDto>(ele)).ToList() ;
        return  response ;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> deleteChracter(int id)
    {
        var serviceResponse=new ServiceResponse<List<GetCharacterDto>>();
        
        try{
            var character=characters.FirstOrDefault(ele=>ele.Id==id);
            
            if(character is null)
                throw new Exception($"id: {id} not found ");

            characters.Remove(character);
            serviceResponse.Data=characters.Select(ch=>autoMap.Map<GetCharacterDto>(ch)).ToList();
        
        }catch(Exception e){
            serviceResponse.success=false;
            serviceResponse.Message=e.Message;    
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllcharacters()
    {
        var  response=new ServiceResponse<List<GetCharacterDto>>();
        var dbCharacters=await db.Characters.ToListAsync();
        response.Data=dbCharacters.Select(ele=>autoMap.Map<GetCharacterDto>(ele)).ToList();
        return response;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
        var  response=new ServiceResponse<GetCharacterDto>();
        var dbCharacters=await db.Characters.ToListAsync();
        var character=dbCharacters.FirstOrDefault(ch => ch.Id == id);
        response.Data=autoMap.Map<GetCharacterDto>(character);
        return response;
    }

    public async Task<ServiceResponse<GetCharacterDto>> updateChracter(UpdateCharacterDto ch)
    {
        var serviceResponse=new ServiceResponse<GetCharacterDto>();
        
        try{
            var character=characters.FirstOrDefault(ele=>ele.Id==ch.Id);

            if(character is null)
                throw new Exception($"id: {ch.Id} not found ");

            character.Name=ch.Name;
            character.HitPoints=ch.HitPoints;
            character.Strength=ch.Strength;
            character.Intelligence=ch.Intelligence;
            character.Class=ch.Class;
        
            serviceResponse.Data=autoMap.Map<GetCharacterDto>(character);
        
        }catch(Exception e){
            serviceResponse.success=false;
            serviceResponse.Message=e.Message;    
        }

        return serviceResponse;

    }
}