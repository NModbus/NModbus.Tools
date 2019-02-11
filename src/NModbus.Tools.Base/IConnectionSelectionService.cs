using NModbus.Tools.Base.Model;

namespace NModbus.Tools.Interfaces
{
    public interface IConnectionSelectionService
    {
        Connection GetConnection();
    }
}
