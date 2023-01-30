using NotesApp.ViewModels;

namespace NotesApp.Views;

public partial class NotesListPage : ContentPage
{
    public NotesListPage()
    {
        InitializeComponent();
        BindingContext = new NotesListViewModel() { Navigation = this.Navigation };
    }
}