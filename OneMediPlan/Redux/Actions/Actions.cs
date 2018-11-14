using System;
using Redux;
using com.b_velop.OneMediPlan.Domain;
using com.b_velop.OneMediPlan.Domain.Enums;
using System.Collections.Generic;

namespace com.b_velop.OneMediPlan.Redux.Actions
{
    public class AddAppSettings : IAction
    {
        public Guid Id { get; set; }
        public MediUser User { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset LastEdit { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
    public class UpdateAppSettings : IAction
    {
        public Guid Id { get; set; }
        public MediUser User { get; set; }
        public DateTimeOffset Created { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }

    public class AddUserAction : IAction
    {
        public Guid Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset LastEdit { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Surename { get; set; }
        public string Email { get; set; }
        public DateTimeOffset Birthday { get; set; }
    }

    public class UpdateUserAction : IAction
    {
        public Guid Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Surename { get; set; }
        public string Email { get; set; }
        public DateTimeOffset Birthday { get; set; }
    }

    public class AddWeekdaysAction : IAction
    {
        public Guid Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset LastEdit { get; set; }
        public Medi Medi { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }

    public class AddMediAction : IAction
    {
        public Medi Medi { get; set; }
        public Guid Id { get; set; }
        public MediUser User { get; set; }
        public string Name { get; set; }
        public double Dosage { get; set; }
        public double Stock { get; set; }
        public DateTimeOffset Created { get; set; }
        public IList<DailyAppointment> DailyAppointments { get; set; }
        public Guid DependsOn { get; set; }
        public string Description { get; set; }
        public DateTimeOffset LastEdit { get; set; }
        public int DosageType { get; set; }
        public IntervallTime IntervallTime { get; set; }
        public double MinimumStock { get; set; }
        public IntervallType IntervallType { get; set; }
        public int PureIntervall { get; set; }
        public DateTimeOffset NextDate { get; set; }
        public DateTimeOffset LastDate { get; set; }
        public DateTimeOffset LastRefill { get; set; }
    }

    public class UpdateMediAction : IAction
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Dosage { get; set; }
        public double Stock { get; set; }
        public DateTimeOffset LastEdit { get; set; }
    }

    public class UpdateMediStockAction : IAction
    {
        public Guid Id { get; set; }
        public double Stock { get; set; }
    }

    public class UpdateMediIntokeAction : IAction
    {
        public Guid Id { get; set; }
        public DateTimeOffset NextDate { get; set; }
    }
}
