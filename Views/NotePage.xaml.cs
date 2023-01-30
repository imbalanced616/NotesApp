using NotesApp.ViewModels;

namespace NotesApp.Views;

public partial class NotePage : ContentPage
{
    public NoteViewModel ViewModel { get; private set; }
    public NotePage(NoteViewModel vm)
    {
        InitializeComponent();
        ViewModel = vm;
        this.BindingContext = ViewModel;
        if (ViewModel.Note.Name != null && ViewModel.Note.TextNote != null) btn1.IsVisible = false;
        else btn2.IsVisible = false;
    }
}