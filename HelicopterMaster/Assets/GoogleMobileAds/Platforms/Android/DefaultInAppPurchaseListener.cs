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

using System;
using UnityEngine;

using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Android
{
    internal class DefaultInAppPurchaseListener : AndroidJavaProxy
    {
        public IDefaultInAppPurchaseProcessor inAppPurchaseProcessor;

        internal DefaultInAppPurchaseListener(IDefaultInAppPurchaseProcessor inAppPurchaseProcessor)
            : base(Utils.PlayStorePurchaseListenerClassName)
        {
            this.inAppPurchaseProcessor = inAppPurchaseProcessor;
        }

        bool isValidPurchase(string sku) {
            return inAppPurchaseProcessor.IsValidPurchase(sku);
        }

        void onInAppPurchaseFinished(AndroidJavaObject result) {
            InAppPurchaseResult wrappedResult = new InAppPurchaseResult(result);
            if (wrappedResult.IsSuccessful && wrappedResult.IsVerified) {
                inAppPurchaseProcessor.ProcessCompletedInAppPurchase(wrappedResult);
            }
        }
    }
}

#endif
