using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SQLI.SRS2.Modules.Menu.Controls
{
    public class ResponsiveItemsControl : Control
    {
        #region Dependency properties

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ResponsiveItemsControl), new PropertyMetadata(Enumerable.Empty<object>(), OnItemsSourceChanged));

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(ResponsiveItemsControl), null);

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ResponsiveItemsControl), new PropertyMetadata(Orientation.Vertical));

        public static readonly DependencyProperty ContextMenuItemTemplateProperty =
            DependencyProperty.Register("ContextMenuItemTemplate", typeof(DataTemplate), typeof(ResponsiveItemsControl), null);

        public static readonly DependencyProperty MoreButtonStyleProperty =
            DependencyProperty.Register("MoreButtonStyle", typeof(Style), typeof(ResponsiveItemsControl), null);

        public static readonly DependencyProperty MoreButtonContentTemplateProperty =
            DependencyProperty.Register("MoreButtonContentTemplate", typeof(DataTemplate), typeof(ResponsiveItemsControl), null);

        public static readonly DependencyProperty VisibleItemsProperty =
            DependencyProperty.Register("VisibleItems", typeof(IEnumerable), typeof(ResponsiveItemsControl), new PropertyMetadata(Enumerable.Empty<object>()));

        public static readonly DependencyProperty HiddenItemsProperty =
            DependencyProperty.Register("HiddenItems", typeof(IEnumerable), typeof(ResponsiveItemsControl), new PropertyMetadata(Enumerable.Empty<object>()));

        #endregion

        private ItemsControl itemsControl;
        private Button moreButton;

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public DataTemplate ContextMenuItemTemplate
        {
            get { return (DataTemplate)GetValue(ContextMenuItemTemplateProperty); }
            set { SetValue(ContextMenuItemTemplateProperty, value); }
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

        public IEnumerable VisibleItems
        {
            get { return (IEnumerable)GetValue(VisibleItemsProperty); }
            set { SetValue(VisibleItemsProperty, value); }
        }

        public IEnumerable HiddenItems
        {
            get { return (IEnumerable)GetValue(HiddenItemsProperty); }
            set { SetValue(HiddenItemsProperty, value); }
        }

        public ResponsiveItemsControl()
        {
            this.SizeChanged += OnResponsiveItemsControlSizeChanged;
        }

        private void OnResponsiveItemsControlSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateItemsCollections(e.NewSize);
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var responsiveItemsControl = (ResponsiveItemsControl)d;
            responsiveItemsControl.OnItemsSourceChanged(e.NewValue, e.OldValue);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            InitializeItemsControlPart();
            InitializeMoreButton();
        }

        private void InitializeItemsControlPart()
        {
            itemsControl = (ItemsControl)GetTemplateChild("PART_ItemsControl");

            if (ItemTemplate == null)
                throw new InvalidOperationException("ItemTemplate must be set");
        }

        private void InitializeMoreButton()
        {
            moreButton = (Button)GetTemplateChild("PART_MoreButton");
            moreButton.Visibility = Visibility.Collapsed;
        }

        protected virtual void OnItemsSourceChanged(object newValue, object oldValue)
        {
            //var newCollection = newValue as IEnumerable<object>;
            //this.VisibleItems = newCollection;
        }


        private void UpdateItemsCollections(Size newSize)
        {
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

            if (allNeededLength > availableLength)
                availableLength -= Orientation == Orientation.Vertical ? moreButton.Height : moreButton.Width;
          
            foreach (var item in ItemsSource)
            {
                if (singleItemLength < availableLength)
                {
                    visibleItemsCollection.Add(item);
                    availableLength -= singleItemLength.Value;
                }
                else
                {
                    hiddenItemsCollection.Add(item);
                }
            }

            ModifyItemsCollectionsIfNeeded(visibleItemsCollection, hiddenItemsCollection);
        }

        private void ModifyItemsCollectionsIfNeeded(Collection<object> visibleItemsCollection, Collection<object> hiddenItemsCollection)
        {
            if (visibleItemsCollection.Count != GetItemsCount(VisibleItems))
            {
                VisibleItems = visibleItemsCollection;
                HiddenItems = hiddenItemsCollection;

                moreButton.Visibility = hiddenItemsCollection.Any() ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private double? GetSingleItemLength()
        {
            double? length = null;

            var singleItem = ItemTemplate.LoadContent();

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
