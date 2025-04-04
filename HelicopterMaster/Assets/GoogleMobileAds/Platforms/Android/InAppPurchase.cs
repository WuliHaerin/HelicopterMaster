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

#if UNITY_ANDROID

using UnityEngine;

using GoogleMobileAds.Api;

namespace GoogleMobileAds.Android
{
    internal class CustomInAppPurchase : ICustomInAppPurchase
    {
        private UnityEngine.AndroidJavaObject purchase;
        public CustomInAppPurchase(AndroidJavaObject purchase)
        {
            this.purchase = purchase;
        }

        public string ProductId {
            get { return purchase.Call<string>("getProductId"); }
        }

        public void RecordResolution(PurchaseResolutionType resolution)
        {
            purchase.Call("recordResolution", (int)resolution);
        }
    }
}

#endif
