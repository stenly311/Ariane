﻿using System;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProofOfConcept
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnLeftMenuHide_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbHideLeftMenu", btnLeftMenuHide, btnLeftMenuShow, pnlLeftMenu);
        }

        private void btnLeftMenuShow_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbShowLeftMenu", btnLeftMenuHide, btnLeftMenuShow, pnlLeftMenu);
        }


        private void btnTopMenuHide_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbHideTopMenu", btnTopMenuHide, btnTopMenuShow, pnlTopMenu);
        }

        private void btnTopMenuShow_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbShowTopMenu", btnTopMenuHide, btnTopMenuShow, pnlTopMenu);
        }


        private void btnRightMenuHide_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbHideRightMenu", btnRightMenuHide, btnRightMenuShow, pnlRightMenu);
        }

        private void btnRightMenuShow_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbShowRightMenu", btnRightMenuHide, btnRightMenuShow, pnlRightMenu);
        }


        private void btnBottomMenuHide_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbHideBottomMenu", btnBottomMenuHide, btnBottomMenuShow, pnlBottomMenu);
        }

        private void btnBottomMenuShow_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbShowBottomMenu", btnBottomMenuHide, btnBottomMenuShow, pnlBottomMenu);
        }


        private void ShowHideMenu(string sb, Button btnHide, Button btnShow, StackPanel pnl)
        {
            Storyboard storyBoard = Resources[sb] as Storyboard;
            storyBoard.Begin(pnl);

            if (sb.Contains("Show"))
            {
                btnHide.Visibility = System.Windows.Visibility.Visible;
                btnShow.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (sb.Contains("Hide"))
            {
                btnHide.Visibility = System.Windows.Visibility.Hidden;
                btnShow.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
