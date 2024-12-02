using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Components;
using Savvy.ZooKeeper.Models;
using Savvy.ZooKeeper.Models.Entities;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;

namespace Savvy.ZooKeeper.Components.Pages
{
    public class BaseGrid<T> : BaseComponent
        where T : class, new()
    {
        [Inject]
        protected ModelContext Database { get; set; } = null!;

        protected readonly DialogSettings DialogParams = new DialogSettings { MinHeight = "400px", Width = "450px" };

        protected readonly ObservableCollection<T> Rows = new();

        protected T? LastRecord;

        protected Syncfusion.Blazor.Grids.Action? LastAction;

        protected virtual IQueryable<T> OnQuery(ModelContext context) => context.Set<T>().AsQueryable();


        protected SfGrid<T>? Grid;

        protected T? SelectedRow { get; set; }

        protected override Task OnInitializedAsync()
        {
            var query = OnQuery(this.Database);

            foreach (var h in query.ToList())
            {
                Rows.Add(h);
            }

            return base.OnInitializedAsync();
        }


        protected void OnGridActionBegin(ActionEventArgs<T> args)
        {
            LastAction = args.RequestType;
            LastRecord = args.RowData;

            PerformAction(false);
        }

        protected void OnFormValidSubmit()
        {
            PerformAction(true);
        }

        public async Task GetSelectedRecords(RowSelectEventArgs<T> args)
        {
            if (this.Grid is null)
            {
                return;
            }

            var rows = await this.Grid.GetSelectedRecordsAsync();

            SelectedRow = rows.FirstOrDefault();

            if (SelectedRow is null)
            {
                return;
            }

            OnSelectedRow(SelectedRow);

            StateHasChanged();
        }

        protected virtual void OnSelectedRow(T selectedRow)
        {
        }

        private void PerformAction(bool isForm)
        {
            if (LastRecord is null)
            {
                return;
            }

            switch (LastAction)
            {
                case Syncfusion.Blazor.Grids.Action.Add when isForm == true:
                    var added = Database.Set<T>().Add(LastRecord);
                    Rows.Add(added.Entity);
                    Database.SaveChanges();
                    break;
                case Syncfusion.Blazor.Grids.Action.Delete:
                    Database.Set<T>().Remove(LastRecord);
                    Database.SaveChanges();
                    break;
                case Syncfusion.Blazor.Grids.Action.BeginEdit:
                    Database.Set<T>().Update(LastRecord);
                    Database.SaveChanges();
                    break;

            }
        }
    }
}