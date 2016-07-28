using System;
using System.Windows.Forms;
using DryIocCastleAop.Aspects;
using DryIoc;
using DryIocCastleAop.Services;

namespace DryIocCastleAop.Views
{
    public partial class MainView : UserControl
    {
        private IContainer _container;

        public MainView(IContainer container)
        {
            _container = container;

            InitializeComponent();
        }

        [ExceptionAspect]
        protected virtual void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                throw new Exception("Field name is blank");
            }

            if (string.IsNullOrEmpty(textBox2.Text))
            {
                throw new Exception("Field login is blank");
            }

            using (var scope = _container.OpenScope())
            {
                var us = scope.Resolve<UserService>();

                us.Validate();
            }
        }

        void button2_Click(object sender, EventArgs e)
        {
            ParentForm.Close();
        }
    }
}
