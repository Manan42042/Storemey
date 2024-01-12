using System.Threading.Tasks;
using Abp.Application.Services;
using Storemey.Configuration.Dto;

namespace Storemey.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}