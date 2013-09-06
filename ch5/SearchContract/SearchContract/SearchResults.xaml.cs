using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Search;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “搜索合同”项模板在 http://go.microsoft.com/fwlink/?LinkId=234240 上提供

namespace SearchContract
{
    /// <summary>
    /// 此页显示全局搜索定向到此应用程序时的搜索结果。
    /// </summary>
    public sealed partial class SearchResults : SearchContract.Common.LayoutAwarePage
    {
        List<Student> students = new List<Student>();
        string searchString;
        SearchPane searchPane;

        public SearchResults()
        {
            this.InitializeComponent();
            BuildStudentList();
            searchPane = SearchPane.GetForCurrentView();
        }

private void BuildStudentList()
{
    students.Add(new Student { Number = 001, Category = "1班", Name = "张三", Symbol = "男", State = "在校" });
    students.Add(new Student { Number = 002, Category = "1班", Name = "小李", Symbol = "男", State = "在校" });
    students.Add(new Student { Number = 003, Category = "1班", Name = "小王", Symbol = "女", State = "校外" });
    students.Add(new Student { Number = 004, Category = "1班", Name = "小周", Symbol = "女", State = "校外" });
    students.Add(new Student { Number = 006, Category = "1班", Name = "老赵", Symbol = "女", State = "在校" });
    students.Add(new Student { Number = 007, Category = "2班", Name = "小陈", Symbol = "男", State = "在校" });
    students.Add(new Student { Number = 008, Category = "2班", Name = "老大", Symbol = "男", State = "在校" });
    students.Add(new Student { Number = 009, Category = "2班", Name = "王二", Symbol = "男", State = "在校" });
    students.Add(new Student { Number = 010, Category = "2班", Name = "陈总", Symbol = "女", State = "校外" });
    students.Add(new Student { Number = 011, Category = "3班", Name = "小刘", Symbol = "女", State = "在校" });
    students.Add(new Student { Number = 012, Category = "3班", Name = "小张", Symbol = "女", State = "在校" });
    students.Add(new Student { Number = 013, Category = "3班", Name = "小朱", Symbol = "男", State = "校外" });
    students.Add(new Student { Number = 014, Category = "3班", Name = "二哥", Symbol = "男", State = "在校" });
    students.Add(new Student { Number = 015, Category = "3班", Name = "大刘", Symbol = "女", State = "在校" });
    students.Add(new Student { Number = 016, Category = "3班", Name = "老五", Symbol = "女", State = "在校" });
}

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            searchPane.SuggestionsRequested += searchPane_SuggestionsRequested;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            searchPane.SuggestionsRequested -= searchPane_SuggestionsRequested;
        }

        void searchPane_SuggestionsRequested(SearchPane sender, SearchPaneSuggestionsRequestedEventArgs args)
        {
             args.Request.SearchSuggestionCollection.AppendQuerySuggestions((from el in students
                                                                            where el.Name.StartsWith(args.QueryText) 
                                                                            orderby el.Name ascending
                                                                            select el.Name).Take(5));
        }


        /// <summary>
        /// 使用在导航过程中传递的内容填充页。在从以前的会话
        /// 重新创建页时，也会提供任何已保存状态。
        /// </summary>
        /// <param name="navigationParameter">最初请求此页时传递给
        /// <see cref="Frame.Navigate(Type, Object)"/> 的参数值。
        /// </param>
        /// <param name="pageState">此页在以前会话期间保留的状态
        /// 字典。首次访问页面时为 null。</param>
protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
{
    searchString = (navigationParameter as String).ToLower();

    var filterList = new List<Filter>();
    filterList.Add(new Filter("All", 0, true));

    // 通过视图模型沟通结果
    this.DefaultViewModel["QueryText"] = '\u201c' + searchString + '\u201d';
    this.DefaultViewModel["Filters"] = filterList;
    this.DefaultViewModel["ShowFilters"] = filterList.Count > 1;
}

        /// <summary>
        /// 在使用处于对齐视图状态的 ComboBox 选择筛选器时进行调用。
        /// </summary>
        /// <param name="sender">ComboBox 实例。</param>
        /// <param name="e">描述如何更改选定筛选器的事件数据。</param>
        void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 确定选定的筛选器
            var selectedFilter = e.AddedItems.FirstOrDefault() as Filter;
            if (selectedFilter != null)
            {
                selectedFilter.Active = true;

                IEnumerable<Student> searchResults = from el in students
                                                     where el.Name.ToLower().Contains(searchString)
                                                     orderby el.Name ascending
                                                     select el;

                this.DefaultViewModel["Results"] = searchResults;

                // Ensure results are found
                object results;
                IEnumerable<Student> resultsCollection;

                if (this.DefaultViewModel.TryGetValue("Results", out results) &&
                    (resultsCollection = results as IEnumerable<Student>) != null &&
                    resultsCollection.Count() != 0)
                {
                    VisualStateManager.GoToState(this, "ResultsFound", true);
                    return;
                }
            }

            // 无搜索结果时显示信息性文本。
            VisualStateManager.GoToState(this, "NoResultsFound", true);
        }

        /// <summary>
        /// 在未对齐的情况下使用 RadioButton 选定筛选器时进行调用。
        /// </summary>
        /// <param name="sender">选定的 RadioButton 实例。</param>
        /// <param name="e">描述如何选定 RadioButton 的事件数据。</param>
        void Filter_Checked(object sender, RoutedEventArgs e)
        {
            // 将更改镜像到对应的 ComboBox 使用的 CollectionViewSource 中
            // 以确保在对齐后反映更改
            if (filtersViewSource.View != null)
            {
                var filter = (sender as FrameworkElement).DataContext;
                filtersViewSource.View.MoveCurrentTo(filter);
            }
        }

        /// <summary>
        /// 描述可用于查看搜索结果的筛选器之一的视图模型。
        /// </summary>
        private sealed class Filter : SearchContract.Common.BindableBase
        {
            private String _name;
            private int _count;
            private bool _active;

            public Filter(String name, int count, bool active = false)
            {
                this.Name = name;
                this.Count = count;
                this.Active = active;
            }

            public override String ToString()
            {
                return Description;
            }

            public String Name
            {
                get { return _name; }
                set { if (this.SetProperty(ref _name, value)) this.OnPropertyChanged("Description"); }
            }

            public int Count
            {
                get { return _count; }
                set { if (this.SetProperty(ref _count, value)) this.OnPropertyChanged("Description"); }
            }

            public bool Active
            {
                get { return _active; }
                set { this.SetProperty(ref _active, value); }
            }

            public String Description
            {
                get { return String.Format("{0} ({1})", _name, _count); }
            }
        }
    }
}
