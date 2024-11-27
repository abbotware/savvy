using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Components;
using Savvy.ZooKeeper.Models;
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