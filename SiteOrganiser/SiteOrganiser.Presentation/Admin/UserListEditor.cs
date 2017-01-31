using SiteOrganiser.Common.Business;
using SiteOrganiser.Common.Management;
using System;
using System.Collections.Generic;

namespace SiteOrganiser.Presentation.Admin
{
    public interface IUserListView
    {
        List<IRegisteredUser> Members { get; set; }
        event EventHandler<EventArgs> Loading;
    }
    public class UserListPresenter : PresenterBase<IAdminBusiness>
    {
        private IUserListView _view;
        public UserListPresenter(IUserListView view)
        {
            _view = view;
            this.Initialize();
        }

        private void Initialize()
        {
            this._view.Loading += _view_Loading;
        }

        private void _view_Loading(object sender, EventArgs e)
        {
            try
            {
                _view.Members = bl.GetMembers();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
