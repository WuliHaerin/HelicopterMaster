/*
\n










*/

// Copyright (C) 2015 Google, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;

using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
    public class RewardBasedVideoAd
    {
        private IRewardBasedVideoAdClient client;
        private static RewardBasedVideoAd instance;

        public static RewardBasedVideoAd Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RewardBasedVideoAd();
                }
                return instance;
            }
        }

        // Creates a Singleton RewardBasedVideoAd.
        private RewardBasedVideoAd()
        {
            client = GoogleMobileAdsClientFactory.BuildRewardBasedVideoAdClient();
            client.CreateRewardBasedVideoAd();

            this.client.OnAdLoaded += (sender, args) =>
                {
                    if (this.OnAdLoaded != null)
                    {
                        this.OnAdLoaded(this, args);
                    }
                };

            this.client.OnAdFailedToLoad += (sender, args) =>
                {
                    if (this.OnAdFailedToLoad != null)
                    {
                        this.OnAdFailedToLoad(this, args);
                    }
                };

            this.client.OnAdOpening += (sender, args) =>
                {
                    if (this.OnAdOpening != null)
                    {
                        this.OnAdOpening(this, args);
                    }
                };

            this.client.OnAdStarted += (sender, args) =>
                {
                    if (this.OnAdStarted != null)
                    {
                        this.OnAdStarted(this, args);
                    }
                };

            this.client.OnAdClosed += (sender, args) =>
                {
                    if (this.OnAdClosed != null)
                    {
                        this.OnAdClosed(this, args);
                    }
                };

            this.client.OnAdLeavingApplication += (sender, args) =>
                {
                    if (this.OnAdLeavingApplication != null)
                    {
                        this.OnAdLeavingApplication(this, args);
                    }
                };

            this.client.OnAdRewarded += (sender, args) =>
                {
                    if (this.OnAdRewarded != null)
                    {
                        this.OnAdRewarded(this, args);
                    }
                };
        }

        // These are the ad callback events that can be hooked into.
        public event EventHandler<EventArgs> OnAdLoaded;

        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        public event EventHandler<EventArgs> OnAdOpening;

        public event EventHandler<EventArgs> OnAdStarted;

        public event EventHandler<EventArgs> OnAdClosed;

        public event EventHandler<Reward> OnAdRewarded;

        public event EventHandler<EventArgs> OnAdLeavingApplication;

        // Loads a new reward based video ad request
        public void LoadAd(AdRequest request, string adUnitId)
        {
            client.LoadAd(request, adUnitId);
        }

        // Determines whether the reward based video has loaded.
        public bool IsLoaded()
        {
            return client.IsLoaded();
        }

        // Shows the reward based video.
        public void Show()
        {
            client.ShowRewardBasedVideoAd();
        }
    }
}
