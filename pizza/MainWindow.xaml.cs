using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace pizza
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public struct PizzaCosts
    {
        public const double Mega = 24.90;
        public const double XXL = 35.90;
        public const double GIGA = 42.90;
    }

    public struct PizzaSizeNames
    {
        public const string MegaName = "Mega 30 cm - 24,90 zł";
        public const string XXLName = "XXL 45 cm - 35,90 zł";
        public const string GigaName = "GIGA 60 cm - 42,90 zł";
    }

    public struct AddonCosts
    {
        public const double BBQ = 3.00;
        public const double Pomidorowy = 3.00;
        public const double Biały = 3.00;
        public const double SerFeta = 5.00;
        public const double Boczek = 5.00;
        public const double Jajko = 5.00;
        public const double Pieczarki = 5.00;
        public const double Szynka = 5.00;
        public const double Oliwki = 5.00;
        public const double Pomidory = 5.00;
        public const double Ananas = 5.00;
    }

    public struct AddonCostsNames
    {
        public const string BBQ = "Extra sos BBQ + 3 zł";
        public const string Pomidorowy = "Extra sos pomidorowy + 3 zł";
        public const string Biały = "Extra sos biały + 3 zł";
        public const string SerFeta = "Ser feta + 5 zł";
        public const string Boczek = "Boczek + 5 zł";
        public const string Jajko = "Jajko + 5 zł";
        public const string Pieczarki = "Pieczarki + 5 zł";
        public const string Szynka = "Szynka + 5 zł";
        public const string Oliwki = "Oliwki + 5 zł";
        public const string Pomidory = "Pomidory + 5 zł";
        public const string Ananas = "Ananas + 5 zł";
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetDefaultSauceAndSize();
            Sum.Content = CalculatePizzaCost();
        }

        private void ResetSkladnikiText()
        {
            Skladniki.Content = "Składniki pizzy:";
        }

        private void SetDefaultSauceAndSize()
        {
            RadioButton sauce = SaucePanel.Children[0] as RadioButton;
            RadioButton size = SizePanel.Children[0] as RadioButton;
            ChozenSauce.Content = sauce.Content;
            ChosenSize.Content = size.Content.ToString().Split(' ')[0];
        }

        private string CalculatePizzaCost()
        {
            if (pizzaListBox.SelectedItems.Count == 0)
            {
                return "Razem: 24,90 zł";
            }

            double cost = 0.0;

            var sizeChecked = SizePanel.Children.OfType<RadioButton>().FirstOrDefault(r => (bool)r.IsChecked);
            var addonsChecked = AddonsPanel.Children.OfType<CheckBox>().Where(f => (bool) f.IsChecked);
            switch (sizeChecked.Content)
            {
                case PizzaSizeNames.MegaName:
                    cost += PizzaCosts.Mega;
                    break;
                case PizzaSizeNames.XXLName:
                    cost += PizzaCosts.XXL;
                    break;
                case PizzaSizeNames.GigaName:
                    cost += PizzaCosts.GIGA;
                    break;
            }

            foreach (var checkBox in addonsChecked)
            {
                switch (checkBox.Content)
                {
                    case AddonCostsNames.BBQ:
                        cost += AddonCosts.BBQ;
                        break;
                    case AddonCostsNames.Pomidorowy:
                        cost += AddonCosts.Pomidorowy;
                        break;
                    case AddonCostsNames.Biały:
                        cost += AddonCosts.Biały;
                        break;
                    case AddonCostsNames.SerFeta:
                        cost += AddonCosts.SerFeta;
                        break;
                    case AddonCostsNames.Boczek:
                        cost += AddonCosts.Boczek;
                        break;
                    case AddonCostsNames.Jajko:
                        cost += AddonCosts.Jajko;
                        break;
                    case AddonCostsNames.Pieczarki:
                        cost += AddonCosts.Pieczarki;
                        break;
                    case AddonCostsNames.Szynka:
                        cost += AddonCosts.Szynka;
                        break;
                    case AddonCostsNames.Oliwki:
                        cost += AddonCosts.Oliwki;
                        break;
                    case AddonCostsNames.Pomidory:
                        cost += AddonCosts.Pomidory;
                        break;
                    case AddonCostsNames.Ananas:
                        cost += AddonCosts.Ananas;
                        break;
                }
            }
            return "Razem: " + cost + "0 zł";
        }

        private void OnListBoxItemClick(object sender, MouseButtonEventArgs e)
        {
            if (ItemsControl.ContainerFromElement(pizzaListBox, e.OriginalSource as DependencyObject) is ListBoxItem item)
            {
                ResetSkladnikiText();
                switch (item.Content)
                {
                    case "Pepperoni":
                        Skladniki.Content += "\nkiełbasa pepperoni, cebula \nbiała i czerwona";
                        break;
                    case "Margherita":
                        Skladniki.Content += "\nsos, ser, oregano";
                        break;
                    case "Hawajska":
                        Skladniki.Content += "\nszynka, ananas";
                        break;
                    case "Americana":
                        Skladniki.Content += "\nsos BBQ, kurczak grilowany, \ncebula biała";
                        break;
                    case "Texas":
                        Skladniki.Content += "\nsos chilli, mięso mielone, \nkukurydza, ser";
                        break;
                    case "Princesa":
                        Skladniki.Content += "\nkrewetki, kurczak grilowany, \nananas";
                        break;
                    case "Vege":
                        Skladniki.Content += "\npieczarki, papryka, cebula\nbiała i czerwona";
                        break;
                }
                ChozenPizza.Content = item.Content;
            }
            Sum.Content = CalculatePizzaCost();
        }

        private void SauceChecked(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is RadioButton)
            {
                if (ChozenSauce != null)
                {
                    var button = e.OriginalSource as RadioButton;
                    ChozenSauce.Content = button.Content;
                }
            }
        }

        private void SizeChecked(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is RadioButton)
            {
                if (ChosenSize != null)
                {
                    var button = e.OriginalSource as RadioButton;
                    ChosenSize.Content = button.Content.ToString().Split(' ')[0];
                    Sum.Content = CalculatePizzaCost();
                }
            }
        }

        private void OnAddonChecked(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is CheckBox)
            {
                if (ChozenAddons != null)
                {
                    var addon = e.OriginalSource as CheckBox;
                    var addonContent = addon.Content.ToString();
                    int countOfAddons = ChozenAddons.Content.ToString().Count(f => f == '\n');
                    if (countOfAddons >= 4)
                    {
                        MessageBox.Show("Możesz wybrać tylko 4 dodatki", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                        addon.IsChecked = false;
                        return;
                    }
                    if ((string)ChozenAddons.Content == "Brak")
                    {
                        ChozenAddons.Content = addonContent.Remove(addonContent.Length - 7) + "\n";
                    }
                    else
                    {
                        ChozenAddons.Content += addonContent.Remove(addonContent.Length - 7) + "\n";
                    }
                    Sum.Content = CalculatePizzaCost();
                }
            }
        }

        private void OnAddonUncheck(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is CheckBox)
            {
                var addonsContainer = ChozenAddons.Content.ToString();
                var addon = e.OriginalSource as CheckBox;
                var addonContent = addon.Content.ToString().Remove(addon.Content.ToString().Length - 7);
                if (addonsContainer.Contains(addonContent))
                {
                    ChozenAddons.Content = addonsContainer.Replace(addonContent + "\n", "");
                }
                Sum.Content = CalculatePizzaCost();
            }
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void OnOrderClick(object sender, RoutedEventArgs e)
        {
            if (pizzaListBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Zaznacz pizzę, aby dokonać zamówienia!", "Alert", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            if (City.Text == "" || Street.Text == "" || HouseNumber.Text == "" || FlatNumber.Text == "")
            {
                MessageBox.Show("Wpisz adres, aby dokonać zamówienia!", "Alert", MessageBoxButton.OK,
                    MessageBoxImage.Asterisk);
                return;
            }

            var sum = Sum.Content.ToString().Replace("Razem: ", "");
            MessageBox.Show("Dziękujemy za złożenie zamówienia! \nPrzygotuj " + sum + " aby odebrać naszą pizzę!",
                "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
