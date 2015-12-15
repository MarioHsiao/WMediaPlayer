﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using DispatcherLibrary;
using MediaLibrary.Annotations;
using MediaLibrary.Audio.SubViews;
using UiLibrary;

namespace MediaLibrary.Audio
{
    public class TabItem : INotifyPropertyChanged
    {
        #region TabItems Properties

        private bool _selected = false;
        public bool Selected { get { return _selected; } internal set { _selected = value; OnPropertyChanged(nameof(Selected)); } }
        public string Name { get; internal set; }

        private UiCommand _onSelected;

        public UiCommand OnSelected
        {
            get { return _onSelected; }
            set
            {
                _onSelected = value;
                OnPropertyChanged(nameof(OnSelected));
            }
        }

        #endregion

        #region Notifier Properties

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public class LibraryClassViewModel : INotifyPropertyChanged
    {
        #region TabItems List

        private List<TabItem> _tabItems = null;

        public List<TabItem> TabItems
        {
            get { return _tabItems; }
            set
            {
                _tabItems = value;
                OnPropertyChanged(nameof(TabItems));
            }
        }

        #endregion

        #region Sub Views

        private Listener _subViewModel = null;

        public Listener SubViewModel
        {
            get { return _subViewModel; }
            set
            {
                _subViewModel = value;
                OnPropertyChanged(nameof(SubViewModel));
            }
        }

        private UserControl _subView = null;

        public UserControl SubView
        {
            get { return _subView; }
            set
            {
                _subView = value;
                OnPropertyChanged(nameof(SubView));
            }
        }

        private readonly Listener[] _subViewModels =
        {
            new AudioTrackViewModel(), new AudioAlbumViewModel(), null
        };

        private readonly UserControl[] _subViews = null;

        #endregion

        #region Contructor

        public LibraryClassViewModel()
        {
            _subViews = new UserControl[] { new AudioTrackView(_subViewModels[0] as AudioTrackViewModel), new AudioAlbumView(_subViewModels[1] as AudioAlbumViewModel), null };
            TabItemsInitialization();
            SubView = _subViews[0];
        }

        private void TabItemsInitialization()
        {
            TabItems = new List<TabItem>
            {
                new TabItem
                {
                    Selected = true,
                    Name = "Tracks",
                    OnSelected = new UiCommand(delegate { SelectTab(0); })
                },
                new TabItem
                {
                    Selected = false,
                    Name = "Albums",
                    OnSelected = new UiCommand(delegate { SelectTab(1); })
                },
                new TabItem
                {
                    Selected = false,
                    Name = "Artists",
                    OnSelected = new UiCommand(delegate { SelectTab(2); })
                }
            };
        }

        private void SelectTab(int item)
        {
            foreach (var tabItem in TabItems)
            {
                tabItem.Selected = false;
            }
            TabItems[item].Selected = true;
            SubViewModel = _subViewModels[item];
            SubView = _subViews[item];
        }

        #endregion

        #region Notifier Properties

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}