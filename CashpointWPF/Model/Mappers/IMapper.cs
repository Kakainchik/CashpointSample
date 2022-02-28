namespace CashpointWPF.Model.Mappers
{
    public interface IMapper<ENTITY, MODEL>
    {
        ENTITY ToEntity(MODEL model);
        MODEL ToModel(ENTITY entity);
    }
}