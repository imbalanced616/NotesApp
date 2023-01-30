using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using NotesApp.Views;

namespace NotesApp.ViewModels
{
    public class NotesListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<NoteViewModel> Notes { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreateNoteCommand { protected set; get; }
        public ICommand DeleteNoteCommand { protected set; get; }
        public ICommand RemoveNoteCommand { protected set; get; }
        public ICommand SaveNoteCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }

        NoteViewModel selectedNote;

        public INavigation Navigation { get; set; }

        public NotesListViewModel()
        {
            Notes = new ObservableCollection<NoteViewModel> 
            { 
                new NoteViewModel {Name="Заметка 1", TextNote="Текст заметки 1"},
                new NoteViewModel {Name="Заметка 2", TextNote="Текст заметки 2"},
                new NoteViewModel {Name="Заметка 2", TextNote="Текст заметки 3"}
            };
            CreateNoteCommand = new Command(CreateNote);
            DeleteNoteCommand = new Command(DeleteNote);
            RemoveNoteCommand = new Command(RemoveNote);
            SaveNoteCommand = new Command(SaveNote);
            BackCommand = new Command(Back);
        }

        public NoteViewModel SelectedNote
        {
            get { return selectedNote; }
            set
            {
                if (selectedNote != value)
                {
                    NoteViewModel tempNote = value;
                    selectedNote = null;
                    OnPropertyChanged("SelectedNote");
                    var n = new NotePage(tempNote);
                    n.Title = "Редактирование заметки";
                    Navigation.PushAsync(n);
                }
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void CreateNote()
        {
            Navigation.PushAsync(new NotePage(new NoteViewModel() { ListViewModel = this }));
        }
        private void Back()
        {
            Navigation.PopAsync();
        }
        private void SaveNote(object noteObject)
        {
            NoteViewModel note = noteObject as NoteViewModel;
            if (note != null && note.IsValid && !Notes.Contains(note))
            {
                Notes.Add(note);
            }
            Back();
        }
        private void DeleteNote(object noteObject)
        {
            NoteViewModel note = noteObject as NoteViewModel;
            if (note != null) Notes.Remove(note);
            Back();
        }
        private void RemoveNote(object noteObj)
        {
            NoteViewModel note = noteObj as NoteViewModel;
            if (note == null) return;
            Notes.Remove(note);
            Back();
        }
    }
}
