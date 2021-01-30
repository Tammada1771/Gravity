using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ART.Gravity.BL;
using ART.Gravity.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace ART.Gravity.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected GravityEntities dc;
        protected IDbContextTransaction transaction;
        public MainWindow()
        {
            InitializeComponent();
            List<BL.Models.Gravity> gravities = new List<BL.Models.Gravity>();
            gravities = GravityManager.LoadForDataBox();

            foreach(var g in gravities)
            {
                var problem = new BL.Models.Gravity();
                problem.Mass1 = g.Mass1;
                problem.Mass2 = g.Mass2;
                problem.Distance = g.Distance;
                problem.Force = g.Force;
                dgData.Items.Add(problem);
            }
            
        }

        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dgData.Items.Clear();

                BL.Models.Gravity gravity = new BL.Models.Gravity();

                gravity.Mass1 = float.Parse(txtMassOne.Text);
                gravity.Mass2 = float.Parse(txtMassTwo.Text);
                gravity.Distance = float.Parse(txtDistance.Text);

                GravityManager.CalcForce(gravity);

                lblForce.Content = gravity.Force.ToString("n25");

                GravityManager.Insert(gravity);

                // Listbox
                List<BL.Models.Gravity> gravities = new List<BL.Models.Gravity>();

                gravities = GravityManager.LoadForDataBox();

                foreach(var g in gravities)
                {
                    var problem = new BL.Models.Gravity();
                    problem.Mass1 = g.Mass1;
                    problem.Mass2 = g.Mass2;
                    problem.Distance = g.Distance;
                    problem.Force = g.Force;
                    dgData.Items.Add(problem);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
