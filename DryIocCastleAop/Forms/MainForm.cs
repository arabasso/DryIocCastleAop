using DryIocCastleAop.Aspects;
using DryIocCastleAop.Views;
using System;
using System.Windows.Forms;

namespace DryIocCastleAop.Forms
{
    public partial class MainForm : Form
    {
        public MainForm(MainView view)
        {
            InitializeComponent();

            Controls.Add(view);

            AutoSize = true;
        }

        [ExceptionAspect]
        protected virtual void MainForm_Load(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

        [TransactionAspect]
        protected virtual void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
