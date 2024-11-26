
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Components;
using Savvy.ZooKeeper.Models;
using Syncfusion.Blazor.Grids;

namespace Savvy.ZooKeeper.Components.Pages
{
    public partial class Habitats
    {
        private readonly ObservableCollection<Habitat> Rows = new();

        private readonly DialogSettings DialogParams = new DialogSettings { MinHeight = "400px", Width = "450px" };

        private Syncfusion.Blazor.Grids.Action? LastAction;

        private Habitat? LastRecord;

        [Inject]
        private ModelContext Database { get; set; } = null!;


        protected override Task OnInitializedAsync()
        {
            
            foreach(var h in Database.Habitats.ToList())
            {
                Rows.Add(h);
            }

            return base.OnInitializedAsync();
        }
                
        private void OnGridActionBegin(ActionEventArgs<Habitat> args)
        {
            LastAction = args.RequestType;
            LastRecord = args.RowData;

            PerformAction(false);
        }

        private void OnFormValidSubmit()
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
                    var added = Database.Habitats.Add(LastRecord);
                    Rows.Add(added.Entity);
                    Database.SaveChanges();
                    break;
                case Syncfusion.Blazor.Grids.Action.Delete:
                    Database.Habitats.Remove(LastRecord);
                    Database.SaveChanges();
                    break;
                case Syncfusion.Blazor.Grids.Action.BeginEdit:
                    Database.Habitats.Update(LastRecord);
                    Database.SaveChanges();
                    break;

            }
        }
    }
}