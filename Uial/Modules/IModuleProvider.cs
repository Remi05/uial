namespace Uial.Modules
{
    public interface IModuleProvider
    {
        Module GetModule(ModuleDefinition moduleDefinition);
    }
}
