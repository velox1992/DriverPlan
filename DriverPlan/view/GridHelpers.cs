using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace DriverPlan.view
{
    public class GridHelpers
    {
        public static readonly DependencyProperty RowCountProperty = DependencyProperty.RegisterAttached(
            "RowCount", typeof(int), typeof(GridHelpers), new PropertyMetadata(-1, RowCountChanged));

        public static int GetRowCount(DependencyObject _Obj)
        {
            return (int) _Obj.GetValue(RowCountProperty);
        }

        public static void SetRowCount(DependencyObject _Obj, int _Value)
        {
            _Obj.SetValue(RowCountProperty, _Value);
        }


        private static void RowCountChanged(DependencyObject _Obj, DependencyPropertyChangedEventArgs _Event)
        {
            if (!(_Obj is Grid) || (int) _Event.NewValue < 0)
            {
                return;
            }

            var hGrid = (Grid) _Obj;
            hGrid.RowDefinitions.Clear();

            for (int i = 0; i < (int)_Event.NewValue; i++)
            {
                hGrid.RowDefinitions.Add(
                    new RowDefinition() {Height = GridLength.Auto}) ;
            }

            SetStarRows(hGrid);
        }

        public static readonly DependencyProperty ColCountProperty = DependencyProperty.RegisterAttached(
            "ColCount", typeof(int), typeof(GridHelpers), new PropertyMetadata(-1, ColCountChanged));

        public static int GetColCount(DependencyObject _Obj)
        {
            return (int)_Obj.GetValue(ColCountProperty);
        }

        public static void SetColCount(DependencyObject _Obj, int _Value)
        {
            _Obj.SetValue(ColCountProperty, _Value);
        }


        private static void ColCountChanged(DependencyObject _Obj, DependencyPropertyChangedEventArgs _Event)
        {
            if (!(_Obj is Grid) || (int)_Event.NewValue < 0)
            {
                return;
            }

            var hGrid = (Grid)_Obj;
            hGrid.ColumnDefinitions.Clear();

            for (int i = 0; i < (int)_Event.NewValue; i++)
            {
                hGrid.ColumnDefinitions.Add(
                    new ColumnDefinition() { Width = GridLength.Auto });
            }

            SetStarColumns(hGrid);
        }

        public static readonly DependencyProperty StarRowsProperty = DependencyProperty.RegisterAttached(
            "StarRows", typeof(string), typeof(GridHelpers), new PropertyMetadata(string.Empty, StarRowsChanged));


        public static string GetStarRows(DependencyObject _Obj)
        {
            return (string) _Obj.GetValue(StarRowsProperty);
        }

        public static void SetStarRows(DependencyObject _Obj, string _Value)
        {
            _Obj.SetValue(StarRowsProperty, _Value);
        }

        public static void StarRowsChanged(DependencyObject _Obj, DependencyPropertyChangedEventArgs _EventArgs)
        {
            if (!(_Obj is Grid) || string.IsNullOrEmpty(_EventArgs.NewValue.ToString()))
                return;

            SetStarRows((Grid)_Obj);
        }


        /// </summary>
        public static readonly DependencyProperty StarColumnsProperty =
            DependencyProperty.RegisterAttached(
                "StarColumns", typeof(string), typeof(GridHelpers),
                new PropertyMetadata(string.Empty, StarColumnsChanged));

        // Get
        public static string GetStarColumns(DependencyObject _Obj)
        {
            return (string)_Obj.GetValue(StarColumnsProperty);
        }

        // Set
        public static void SetStarColumns(DependencyObject _Obj, string _Value)
        {
            _Obj.SetValue(StarColumnsProperty, _Value);
        }

        // Change Event - Makes specified Column's Width equal to Star
        public static void StarColumnsChanged(DependencyObject _Obj, DependencyPropertyChangedEventArgs _EventArgs)
        {
            if (!(_Obj is Grid) || string.IsNullOrEmpty(_EventArgs.NewValue.ToString()))
                return;

            SetStarColumns((Grid)_Obj);
        }

        private static void SetStarColumns(Grid _Grid)
        {
            string[] hStarColumns =
                GetStarColumns(_Grid).Split(',');

            for (int i = 0; i < _Grid.ColumnDefinitions.Count; i++)
            {
                if (((IList) hStarColumns).Contains(i.ToString()))
                    _Grid.ColumnDefinitions[i].Width =
                        new GridLength(1, GridUnitType.Star);
            }
        }

        private static void SetStarRows(Grid _Grid)
        {
            string[] hStarRows =
                GetStarRows(_Grid).Split(',');

            for (int i = 0; i < _Grid.RowDefinitions.Count; i++)
            {
                if (((IList) hStarRows).Contains(i.ToString()))
                    _Grid.RowDefinitions[i].Height =
                        new GridLength(1, GridUnitType.Star);
            }
        }



    }
}