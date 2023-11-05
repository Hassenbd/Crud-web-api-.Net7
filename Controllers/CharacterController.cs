using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
    private static  List<Character> characters = new List<Character>
    {
        new Character(),
        new Character(){Id=1,Name="hassen"}
    };

    private readonly ICharactersService service; 

    public CharacterController(ICharactersService CharService)
    {
        service=CharService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetAll()
    {
        return Ok(await service.GetAllcharacters());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>> > GetSingle(int id)
    {
        return Ok(await service.GetCharacterById(id)) ;
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> addNewCharacter(AddCharacterDto ch)
    {
        return Ok(await service.addChracter(ch));
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> updateChracter(UpdateCharacterDto ch)
    {
        var response=await service.updateChracter(ch);
        if(response.Data is null)
            return NotFound(response);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> deleteChracter(int id)
    {
        var response=await service.deleteChracter(id);
        if(response.Data is null)
            return NotFound(response);
        return Ok(response);
    }

}
