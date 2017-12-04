using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OAuthOpenPlaygroundClient
{
    public partial class frmPreferences : Form
    {
        public frmPreferences()
        {
            InitializeComponent();
        }

        private void frmPreferences_Load(object sender, EventArgs e)
        {
            txtAuthEndpoint.Text = Preferences.AuthorizationEndpoint;
            txtTokenEndpoint.Text = Preferences.TokenEndpoint;
            txtUserInfoEndpoint.Text = Preferences.UserInfoEndpoint;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Preferences.AuthorizationEndpoint = txtAuthEndpoint.Text;
            Preferences.TokenEndpoint = txtTokenEndpoint.Text;
            Preferences.UserInfoEndpoint = txtUserInfoEndpoint.Text;

            this.Close();
        }
    }
}
