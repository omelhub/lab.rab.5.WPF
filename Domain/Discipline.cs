using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lab.rab._5.WPF.Domain;

public class Discipline : INotifyPropertyChanged
{
    public Discipline()
    {
    }

    public Discipline(string? name, bool status)
    {
        Name = name;
        Status = status;
    }

    private string? _name;
    public string? Name
    {
        get => _name;
        set { _name = value; NotifyPropertyChanged(); }

    }

    private bool _status;
    public bool Status
    {
        get => _status;
        set { _status = value; NotifyPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyname = "")
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
