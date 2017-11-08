using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyAlbumExplorer
{
    public partial class ExplorerForm : Form
    {
        public ExplorerForm()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            // Assign title bar 
            Version v = new Version(Application.ProductVersion);
            this.Text = string.Format("My AlbumExplorer {0:#. 2:#}",
                                      v.Major, v.Minor);

            base.OnLoad(e);
        }
    }
}
