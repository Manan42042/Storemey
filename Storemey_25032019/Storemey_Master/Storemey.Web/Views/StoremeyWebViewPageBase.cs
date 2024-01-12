using Abp.Web.Mvc.Views;

namespace Storemey.Web.Views
{
    public abstract class StoremeyWebViewPageBase : StoremeyWebViewPageBase<dynamic>
    {

    }

    public abstract class StoremeyWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected StoremeyWebViewPageBase()
        {
            LocalizationSourceName = StoremeyConsts.LocalizationSourceName;
        }
    }
}