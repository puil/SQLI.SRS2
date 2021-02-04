using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SQLI.SRS2.Core.Controls
{
    public class ResponsiveButtonsControl : Control
    {
        #region Dependency properties

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ResponsiveButtonsControl), new PropertyMetadata(Enumerable.Empty<object>(), OnItemsSourceChanged));

        public static readonly DependencyProperty VisibleItemTemplateProperty =
            DependencyProperty.Register("VisibleItemTemplate", typeof(DataTemplate), typeof(ResponsiveButtonsControl), null);

        public static readonly DependencyProperty HiddenItemTemplateProperty =
            DependencyProperty.Register("HiddenItemTemplate", typeof(DataTemplate), typeof(ResponsiveButtonsControl), null);

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ResponsiveButtonsControl), new PropertyMetadata(Orientation.Vertical));

        public static readonly DependencyProperty ContextMenuItemStyleProperty =
            DependencyProperty.Register("ContextMenuItemStyle", typeof(Style), typeof(ResponsiveButtonsControl), null);

        public static readonly DependencyProperty MoreButtonStyleProperty =
            DependencyProperty.Register("MoreButtonStyle", typeof(Style), typeof(ResponsiveButtonsControl), null);

        public static readonly DependencyProperty MoreButtonContentTemplateProperty =
            DependencyProperty.Register("MoreButtonContentTemplate", typeof(DataTemplate), typeof(ResponsiveButtonsControl), null);

        public static readonly DependencyProperty MoreButtonToolTipProperty =
            DependencyProperty.Register("MoreButtonToolTip", typeof(string), typeof(ResponsiveButtonsControl), null);

        public static readonly DependencyProperty InspectorItemCommandProperty =
            DependencyProperty.Register("InspectorItemCommand", typeof(ICommand), typeof(ResponsiveButtonsControl), new PropertyMetadata(null));

        #endregion

        #region Properties

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public DataTemplate VisibleItemTemplate
        {
            get { return (DataTemplate)GetValue(VisibleItemTemplateProperty); }
            set { SetValue(VisibleItemTemplateProperty, value); }
        }

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public DataTemplate HiddenItemTemplate
        {
            get { return (DataTemplate)GetValue(HiddenItemTemplateProperty); }
            set { SetValue(HiddenItemTemplateProperty, value); }
        }

        public Style ContextMenuItemStyle
        {
            get { return (Style)GetValue(ContextMenuItemStyleProperty); }
            set { SetValue(ContextMenuItemStyleProperty, value); }
        }

        public Style MoreButtonStyle
        {
            get { return (Style)GetValue(MoreButtonStyleProperty); }
            set { SetValue(MoreButtonStyleProperty, value); }
        }

        public DataTemplate MoreButtonContentTemplate
        {
            get { return (DataTemplate)GetValue(MoreButtonContentTemplateProperty); }
            set { SetValue(MoreButtonContentTemplateProperty, value); }
        }

        public string MoreButtonToolTip
        {
            get { return (string)GetValue(MoreButtonToolTipProperty); }
            set { SetValue(MoreButtonToolTipProperty, value); }
        }

        public ICommand InspectorItemCommand
        {
            get { return (ICommand)GetValue(InspectorItemCommandProperty); }
            set { SetValue(InspectorItemCommandProperty, value); }
        }

        #endregion

        private CustomItemsControl itemsControl;
        private Button moreButton;
        private ContextMenu contextMenu;
        private Size? currentSize;
        private bool firstTimeFillingItems = true;

        private readonly ObservableCollection<object> visibleItems;
        private readonly ObservableCollection<object> hiddenItems;

        public ResponsiveButtonsControl()
        {
            visibleItems = new ObservableCollection<object>();
            hiddenItems = new ObservableCollection<object>();

            this.SizeChanged += OnResponsiveButtonsControlSizeChanged;
        }

        private void OnResponsiveButtonsControlSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateItemsCollections(e.NewSize);
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var responsiveButtonsControl = (ResponsiveButtonsControl)d;
            responsiveButtonsControl.UpdateItemsCollections();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            InitializeComponents();
        }

        private void InitializeComponents()
        {
            itemsControl = (CustomItemsControl)GetTemplateChild("PART_ItemsControl");
            moreButton = (Button)GetTemplateChild("PART_MoreButton");
            contextMenu = (ContextMenu)GetTemplateChild("PART_ContextMenu");

            itemsControl.ItemsSource = visibleItems;
            contextMenu.ItemsSource = hiddenItems;

            moreButton.Visibility = Visibility.Collapsed;
        }

        private void UpdateItemsCollections()
        {
            if (currentSize != null)
                UpdateItemsCollections(currentSize.Value);
        }

        private void UpdateItemsCollections(Size newSize)
        {
            currentSize = newSize;

            // Fill VisibleItems and HiddenItems collections depending on width or height (according to orientation) and available space
            var length = Orientation == Orientation.Vertical ? newSize.Height : newSize.Width;
            var availableLength = length;

            var singleItemLength = GetSingleItemLength();
            if (singleItemLength == null)
                return;

            var itemsCount = GetItemsCount(ItemsSource);
            var allNeededLength = itemsCount * singleItemLength.Value;

            var visibleItemsCollection = new Collection<object>();
            var hiddenItemsCollection = new Collection<object>();

            int visibleItemsCount = 0;

            if (allNeededLength > availableLength)
                availableLength -= Orientation == Orientation.Vertical ? moreButton.Height : moreButton.Width;

            foreach (var item in ItemsSource)
            {
                if (singleItemLength < availableLength)
                {
                    visibleItemsCount++;
                    visibleItemsCollection.Add(item);
                    availableLength -= singleItemLength.Value;
                }
                else
                {
                    hiddenItemsCollection.Add(item);
                }
            }

            ModifyItemsCollectionsIfNeeded(visibleItemsCount);
        }

        private void ModifyItemsCollectionsIfNeeded(int visibleItemsCount)
        {
            if (visibleItemsCount != visibleItems.Count)
            {
                int i = 0;

                foreach (var item in ItemsSource)
                {
                    if (i >= visibleItemsCount)
                    {
                        if (!hiddenItems.Contains(item))
                        {
                            if (firstTimeFillingItems)
                                hiddenItems.Add(item);
                            else
                                hiddenItems.Insert(0, item);
                        }

                        if (visibleItems.Contains(item))
                            visibleItems.Remove(item);
                    }
                    else
                    {
                        if (!visibleItems.Contains(item))
                            visibleItems.Add(item);

                        if (hiddenItems.Contains(item))
                            hiddenItems.Remove(item);
                    }

                    i++;
                }

                firstTimeFillingItems = false;

                moreButton.Visibility = hiddenItems.Any() ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private double? GetSingleItemLength()
        {
            double? length = null;

            var singleItem = VisibleItemTemplate.LoadContent();

            if (singleItem is FrameworkElement el)
                length = Orientation == Orientation.Vertical ? el.Height : el.Width;

            return length != null && !double.IsNaN(length.Value) ? length.Value : (double?)null;
        }

        private int GetItemsCount(IEnumerable source)
        {
            if (source is ICollection c)
                return c.Count;

            int result = 0;
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
                result++;

            return result;
        }
    }
}
