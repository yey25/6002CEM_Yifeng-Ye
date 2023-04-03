namespace Shopping.App.Control;

public partial class QuantityContent : ContentView
{
    public static readonly BindableProperty QuantityProperty
        = BindableProperty.Create(nameof(Quantity), typeof(int),
                                typeof(QuantityContent), 1);

    public int Quantity
    {
        get => (int)GetValue(QuantityProperty);
        set
        {
            if (value <= 0)
                value = 1;

            SetValue(QuantityProperty, value);
            OnPropertyChanged(nameof(IsMinusEnabled));
        }
    }

    public bool IsMinusEnabled
        => Quantity > 1;


    public QuantityContent()
	{
		InitializeComponent();
    }


    private void MinusButton_Clicked(object sender, EventArgs e)
    {
        Quantity--;
    }

    private void AddButton_Clicked(object sender, EventArgs e)
    {
        Quantity++;
    }
}