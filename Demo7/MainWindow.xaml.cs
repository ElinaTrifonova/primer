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

namespace Demo7
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SportEntities db;
        List<Product> products;
        List list;
        
        public MainWindow()
        {
            InitializeComponent();
            db = new SportEntities();
            var prod = db.Product.ToList();
            lstw.ItemsSource = prod;
        }

        private void btnLook_Click(object sender, RoutedEventArgs e)
        {
            Spisoc sps = new Spisoc();
            sps.Show();
            
        }

        public List<Product> sortSpisok(List<Product> produc )
        {
            List<Product> prodSort=produc; 
            int indexSort = cmbSort.SelectedIndex;
            if (indexSort == 0)
            {
                var prodVozr = produc.OrderBy(p => p.ProductCost);
                prodSort = prodVozr.ToList();
                lstw.ItemsSource = prodSort;
            }
            if (indexSort == 1)
            {
                var prodVozr = produc.OrderByDescending(p => p.ProductCost);
                prodSort = prodVozr.ToList();
                lstw.ItemsSource = prodSort;
            }

            return prodSort;
        }
        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Product> PromRez;
            List<Product> PromRez1;
            db = new SportEntities();
            var prod = db.Product.ToList();
            products = prod;
            PromRez=sortSpisok(products);
            PromRez1 = PromRez;
            if (txtFind.Text != "")
            {
                PromRez1 = findSpisok(PromRez);
            }
            if (PromRez1.Count() == 0) return;
            if (cmbFiltr.SelectedIndex != -1)
            {
                filtrSpisok(PromRez1);
            }

        }

        public List<Product> filtrSpisok(List<Product> produc)
        {
            int index = cmbFiltr.SelectedIndex;
            List<Product> prodFiltr=produc;

            switch (cmbFiltr.SelectedIndex)
            {
                case 0:
                    {
                         prodFiltr = produc.Select(p => p).ToList();
                        

                    }
                    break;
                case 1:
                    {
                        prodFiltr = produc.Where(p => p.ProductDiscountAmount <= 3).ToList();
                        lstw.ItemsSource = prodFiltr;
                    }
                    break;
                case 2:
                    {
                        prodFiltr = produc.Where(p => (p.ProductDiscountAmount >= 4) && (p.ProductDiscountAmount < 6)).ToList();
                        lstw.ItemsSource = prodFiltr;
                    }
                    break;
                case 3:
                    {
                        prodFiltr = produc.Where(p => p.ProductDiscountAmount >= 6).ToList();
                        lstw.ItemsSource = prodFiltr;
                    }
                    break;
            }
            lstw.ItemsSource = prodFiltr;
            return prodFiltr;
        }
        private void cmbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Product> PromRez;
            List<Product> PromRez1;
            db = new SportEntities();
            var prod = db.Product.ToList();
            PromRez=filtrSpisok(prod);
            if (PromRez.Count() == 0) return;
            PromRez1 = PromRez;
            if (txtFind.Text!="")
            {
                PromRez1 = findSpisok(PromRez);
            }
            if (PromRez1.Count()!=0 && cmbSort.SelectedIndex != -1)
            {
                sortSpisok(PromRez1);
            }

        }
        public List<Product> findSpisok(List<Product> produc)
        {
            var currentName = txtFind.Text;
            var findName = produc.Where(p => p.ProductName.StartsWith(currentName)).ToList();            
            lstw.ItemsSource = findName;
            return findName;
        }
        private void txtFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Product> PromRez;
            db = new SportEntities();
            var prod = db.Product.ToList();
            var prodFin=findSpisok(prod);
            if (prodFin.Count == 0) return;
            PromRez = prodFin;
            if (cmbFiltr.SelectedIndex!=-1)
            {
                PromRez = filtrSpisok(prodFin);
            }
            if (cmbSort.SelectedIndex!=-1)
            {
                sortSpisok(PromRez);
            }

        }
    }
}
