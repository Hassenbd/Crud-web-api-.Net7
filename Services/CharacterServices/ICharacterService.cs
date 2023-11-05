public interface ICharactersService
{
    Task<ServiceResponse<List<GetCharacterDto>>> GetAllcharacters();

    Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);

    Task<ServiceResponse<List<GetCharacterDto>>> addChracter(AddCharacterDto ch);
    
    Task<ServiceResponse<GetCharacterDto>> updateChracter(UpdateCharacterDto ch);

    Task<ServiceResponse<List<GetCharacterDto>>> deleteChracter(int id);
}
