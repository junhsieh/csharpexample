namespace AutoCompleteMVVMWPFToolKit.ViewModel
{
    class ObjectOpt : ObservableBase
    {
        public object ID { get; set; }
        public string Text { get; set; }
        private bool _IsSelected;
        public bool IsSelected
        {
            get { return this._IsSelected; }
            set
            {
                if (this._IsSelected != value)
                {
                    this._IsSelected = value;
                    base.NotifyPropertyChanged("IsSelected");
                }
            }
        }
    }
}
