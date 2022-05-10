using GalaSoft.MvvmLight.Command;
using lab.rab._5.WPF.Domain;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab.rab._5.WPF.BL;

internal class Logical : INotifyPropertyChanged
{
    public Logical()
    {
        Disciplines.Add(new Discipline() { Name = "ВВПД", Status = false });
        Disciplines.Add(new Discipline() { Name = "Высшая математика", Status = false });
        Disciplines.Add(new Discipline() { Name = "Дискретная математика", Status = false });
        Disciplines.Add(new Discipline() { Name = "Макроэкономика", Status = false });
        Disciplines.Add(new Discipline() { Name = "Всеобщая история", Status = true });
        Disciplines.Add(new Discipline() { Name = "Иностранный язык", Status = false });
        Disciplines.Add(new Discipline() { Name = "Программирование", Status = true });
        Disciplines.Add(new Discipline() { Name = "Системный анализ", Status = false });
        Disciplines.Add(new Discipline() { Name = "Физическая культура", Status = false });
        Disciplines.Add(new Discipline() { Name = "Практика", Status = false });

        foreach (var discipline in Disciplines)
        {
            SelectedDisciplines.Add((Discipline)discipline);
            ShowingDisciplines.Add((Discipline)discipline);
        }

        NewDiscipline = new();

        ChangeStatusCommand = new RelayCommand(ChangeStatus);
        DeleteDisciplineCommand = new RelayCommand(DeleteDiscipline);
        DisplayCommand = new RelayCommand(Display);
        AddDisciplineCommand = new RelayCommand(AddDiscipline);
        SortingAllCommand = new RelayCommand(SortingAll);
        SortingDoneCommand = new RelayCommand(SortingDone);
        SortingNotYetCommand = new RelayCommand(SortingNotYet);

        //StatusToColorConverter statusToColorConverter = new StatusToColorConverter();
    }
    public ObservableCollection<Discipline> Disciplines { get; set; } = new();
    public ObservableCollection<Discipline> ShowingDisciplines { get; set; } = new();
    public ObservableCollection<Discipline> SelectedDisciplines { get; set; } = new();

public List<bool> Statuses { get; set; } = new()
    {
        true, false
    };

    public Discipline SelectedDiscipline { get; set; }

    private Discipline newDiscipline = new();
    public Discipline NewDiscipline
    { 
        get => newDiscipline;
        set
        {
            newDiscipline = value;
            OnPropertyChanged("NewDiscipline");
        } 
    }

    //public Button SelectedSorting { get; set; }

    public RelayCommand ChangeStatusCommand { get; set; }
    public RelayCommand DeleteDisciplineCommand { get; set; }
    public RelayCommand SaveCommand { get; set; }
    public RelayCommand DisplayCommand { get; set; }
    public RelayCommand AddDisciplineCommand { get; set; }
    public RelayCommand SortingAllCommand { get; set; }
    public RelayCommand SortingDoneCommand { get; set; }
    public RelayCommand SortingNotYetCommand { get; set; }

    private void ChangeStatus()
    {
        foreach (Discipline discipline in Disciplines)
        {
            if (discipline.Equals(SelectedDiscipline))
            {
                discipline.Status = !discipline.Status;
                break;
            }
        }
        bool a = false; bool d = false; bool n = false;
        foreach (Discipline discipline in SelectedDisciplines)
        {
            if (discipline.Status)
                d = true;
            else
                n = true;
        }
        if ((d && n)) // | (!d && !n)
            a = true;
        foreach (Discipline discipline in SelectedDisciplines)
        {
            if (discipline.Equals(SelectedDiscipline))
            {
                if (a)
                    discipline.Status = !discipline.Status;
                else
                    SelectedDisciplines.Remove(discipline);
                break;
            }
        }
        //Display();
        //SelectedDiscipline.Status = !SelectedDiscipline.Status;
        //Disciplines.Clear();
        //SelectedDisciplines.Clear();
        //foreach (Discipline discipline in ShowingDisciplines)
        //{
        //    Disciplines.Add(discipline);
        //    SelectedDisciplines.Add(discipline);
        //}
    }

    private void DeleteDiscipline()
    {
        Disciplines.Remove(SelectedDiscipline);
        SelectedDisciplines.Remove(SelectedDiscipline);
        ShowingDisciplines.Remove(SelectedDiscipline);
    }

    //private void Save()
    //{
    //    SaveFileDialog file = new SaveFileDialog();
    //    file.DefaultExt = ".txt";
    //    file.Filter = "Test files|*.txt";
    //    if (file.ShowDialog == System.Windows.Forms.DialogResult.OK && file.FileName.Length > 0)
    //    {
    //        using (StreamWriter sw = new(file.FileName, true))
    //        {
    //            sw.WriteLine(MainWindow.Subj Subjects.Text);
    //            sw.Close();
    //        }
    //    }
    //}

    private void Display()
    {
        ShowingDisciplines.Clear();
        foreach (var discipline in SelectedDisciplines)
        {
            ShowingDisciplines.Add((Discipline)discipline);
        }
    }

    public void AddDiscipline()
    {
        bool a = false; bool d = false; bool n = false;
        foreach (var discipline in SelectedDisciplines)
        {
            if (discipline.Status)
                d = true;
            else
                n = true;
        }
        if (d && n | !d && !n)
            a = true;

        bool i = false;
        foreach (var discipline in Disciplines)
        {
            if (NewDiscipline.Equals(discipline))
                i = true;
        }
        if (!i && NewDiscipline.Name != null)
        {
            Disciplines.Add(new Discipline(NewDiscipline.Name, NewDiscipline.Status));
            if (a | (d && NewDiscipline.Status) | (n && !NewDiscipline.Status))
                SelectedDisciplines.Add(NewDiscipline);
            Display();
        }
        NewDiscipline = new();


    }

    public void SortingAll()
    {
        SelectedDisciplines.Clear();
        foreach (var discipline in Disciplines)
        {
            SelectedDisciplines.Add((Discipline)discipline);
        }
    }
    public void SortingDone()
    {
        SelectedDisciplines.Clear();
        foreach (var discipline in Disciplines)
        {
            if(discipline.Status)
                SelectedDisciplines.Add((Discipline)discipline);
        }
    }
    public void SortingNotYet()
    {
        SelectedDisciplines.Clear();
        foreach (var discipline in Disciplines)
        {
            if (!discipline.Status)
                SelectedDisciplines.Add((Discipline)discipline);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyname)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
    }
}
