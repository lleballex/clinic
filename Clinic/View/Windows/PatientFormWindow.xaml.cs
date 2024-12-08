﻿using Clinic.ViewModel.Main;
using DAL.Entities;
using System.Windows;

namespace Clinic.View.Windows
{
    public partial class PatientFormWindow : Window
    {
        private Action OnRepoChange;

        public PatientFormWindow(Action onRepoChange, Patient? patient = null)
        {
            InitializeComponent();

            OnRepoChange = onRepoChange;

            DataContext = new PatientFormVM(patient, OnSubmit, OnCancel);
        }

        private void OnSubmit()
        {
            Close();
            OnRepoChange();
        }

        private void OnCancel()
        {
            Close();
        }
    }
}
