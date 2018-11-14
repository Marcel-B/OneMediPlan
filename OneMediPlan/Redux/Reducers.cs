using System;
using System.Collections.Immutable;
using System.Linq;
using com.b_velop.OneMediPlan.Domain;
using com.b_velop.OneMediPlan.Redux.Actions;
using com.b_velop.OneMediPlan.Redux.States;
using Redux;

namespace com.b_velop.OneMediPlan.Redux
{
    public static class Reducers
    {
        public static ImmutableArray<MediUser> UpdateMediUserReducer(
            ImmutableArray<MediUser> previousState,
            UpdateUserAction action)
        {
            var user = previousState.First();
            return previousState
                .Replace(user, new MediUser
                {
                    Id = action.Id,
                    LastEdit = DateTimeOffset.Now,
                    Created = action.Created,
                    Birthdate = action.Birthday,
                    Name = action.Name,
                    Surename = action.Surename,
                    Email = action.Email,
                    Username = action.Username,
                });
        }
        public static ImmutableArray<MediUser> AddMediUserReducer(
        ImmutableArray<MediUser> previousState,
            AddUserAction action)
        {
            return previousState
                .Insert(0, new MediUser
                {
                    Id = action.Id,
                    LastEdit = action.LastEdit,
                    Created = action.Created,
                    Birthdate = action.Birthday,
                    Name = action.Name,
                    Surename = action.Surename,
                    Email = action.Email,
                    Username = action.Username,
                });
        }

        public static ImmutableArray<Weekdays> AddWeekdayReducer(
        ImmutableArray<Weekdays> previousState,
            AddWeekdaysAction action)
        {
            return previousState
                .Insert(0, new Weekdays
                {
                    Id = action.Id,
                    Created = action.Created,
                    LastEdit = action.LastEdit,
                    Medi = action.Medi,
                    Monday = action.Monday,
                    Tuesday = action.Tuesday,
                    Wednesday = action.Wednesday,
                    Thursday = action.Thursday,
                    Friday = action.Friday,
                    Saturday = action.Saturday,
                    Sunday = action.Sunday
                });
        }

        public static ImmutableArray<Medi> AddMediReducer(
            ImmutableArray<Medi> previousState,
            AddMediAction action)
        {
            return previousState
                .Insert(0, new Medi
                {
                    Id = action.Id,
                    User = action.User,
                    Name = action.Name,
                    Dosage = action.Dosage,
                    Stock = action.Stock,
                    Created = action.Created,
                    DailyAppointments = action.DailyAppointments,
                    DependsOn = action.DependsOn,
                    Description = action.Description,
                    LastEdit = DateTimeOffset.Now,
                    DosageType = action.DosageType,
                    IntervallTime = action.IntervallTime,
                    MinimumStock = action.MinimumStock,
                    IntervallType = action.IntervallType,
                    PureIntervall = action.PureIntervall,
                    NextDate = action.NextDate,
                    LastDate = action.LastDate,
                    LastRefill = action.LastRefill
                }).Sort();
        }

        public static ImmutableArray<Medi> UpdateMediIntokeReducer(
            ImmutableArray<Medi> previousState,
            UpdateMediIntokeAction action)
        {
            var mediToEdit = previousState.Single(medi => medi.Id == action.Id);

            return previousState
                .Replace(mediToEdit, new Medi
                {
                    Id = mediToEdit.Id,
                    User = mediToEdit.User,
                    Name = mediToEdit.Name,
                    Dosage = mediToEdit.Dosage,
                    Stock = mediToEdit.Stock,
                    Created = mediToEdit.Created,
                    DailyAppointments = mediToEdit.DailyAppointments,
                    DependsOn = mediToEdit.DependsOn,
                    Description = mediToEdit.Description,
                    LastEdit = DateTimeOffset.Now,
                    DosageType = mediToEdit.DosageType,
                    IntervallTime = mediToEdit.IntervallTime,
                    MinimumStock = mediToEdit.MinimumStock,
                    IntervallType = mediToEdit.IntervallType,
                    PureIntervall = mediToEdit.PureIntervall,
                    NextDate = action.NextDate,
                    LastDate = DateTimeOffset.Now,
                    LastRefill = mediToEdit.LastRefill
                }).Sort();
        }

        public static ImmutableArray<Medi> UpdateStockReducer(
            ImmutableArray<Medi> previousState,
            UpdateMediStockAction action)
        {
            var mediToEdit = previousState.Single(medi => medi.Id == action.Id);

            return previousState
                .Replace(mediToEdit, new Medi
                {
                    Id = mediToEdit.Id,
                    Name = mediToEdit.Name,
                    Dosage = mediToEdit.Dosage,
                    Stock = action.Stock,
                    Created = mediToEdit.Created,
                    DailyAppointments = mediToEdit.DailyAppointments,
                    DependsOn = mediToEdit.DependsOn,
                    Description = mediToEdit.Description,
                    LastEdit = DateTimeOffset.Now,
                    DosageType = mediToEdit.DosageType,
                    IntervallTime = mediToEdit.IntervallTime,
                    MinimumStock = mediToEdit.MinimumStock,
                    IntervallType = mediToEdit.IntervallType,
                    PureIntervall = mediToEdit.PureIntervall,
                    NextDate = mediToEdit.NextDate,
                    LastDate = mediToEdit.LastDate,
                    LastRefill = mediToEdit.LastRefill
                }).Sort();
        }

        public static ImmutableArray<Medi> UpdateMediReducer(
            ImmutableArray<Medi> previousState, 
            UpdateMediAction action)
        {
            var mediToEdit = previousState.Single(medi => medi.Id == action.Id);

            return previousState
                .Replace(mediToEdit, new Medi
                {
                    Id = Guid.NewGuid(),
                    Name = action.Name,
                    Dosage = action.Dosage,
                    Stock = action.Stock,
                    Created = DateTimeOffset.Now,
                }).Sort();
        }

        public static ImmutableArray<Medi> MedisReducer(
            ImmutableArray<Medi> previousState, 
            IAction action)
        {
            if (action is AddMediAction)
                return AddMediReducer(previousState, (AddMediAction)action);
            if (action is UpdateMediAction)
                return UpdateMediReducer(previousState, (UpdateMediAction)action);
            if (action is UpdateMediStockAction)
                return UpdateStockReducer(previousState, (UpdateMediStockAction)action);
            if (action is UpdateMediIntokeAction)
                return UpdateMediIntokeReducer(previousState, (UpdateMediIntokeAction)action);
            return previousState;
        }

        public static ImmutableArray<Weekdays> WeekdaysReducer(
            ImmutableArray<Weekdays> previousState,
            IAction action)
        {
            if (action is AddWeekdaysAction)
                return AddWeekdayReducer(previousState, (AddWeekdaysAction)action);
            return previousState;
        }

        public static ImmutableArray<MediUser> UsersReducer(
            ImmutableArray<MediUser> previousState,
            IAction action)
        {
            if (action is AddUserAction)
                return AddMediUserReducer(previousState, (AddUserAction)action);
            if (action is UpdateUserAction)
                return UpdateMediUserReducer(previousState, (UpdateUserAction)action);
            return previousState;
        }

        public static ApplicationState ReduceApplication(
            ApplicationState previousState, 
            IAction action)
        {
            return new ApplicationState
            {
                Users = UsersReducer(previousState.Users, action),
                Weekdays = WeekdaysReducer(previousState.Weekdays, action),
                Medis = MedisReducer(previousState.Medis, action)
            };
        }
    }
}
