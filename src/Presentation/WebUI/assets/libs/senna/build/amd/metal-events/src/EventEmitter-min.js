define(["exports","metal/src/metal","./EventHandle"],function(e,t,n){"use strict";function r(e){return e&&e.__esModule?e:{"default":e}}function i(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function s(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function a(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}function u(e){return e=e||[],Array.isArray(e)?e:[e]}Object.defineProperty(e,"__esModule",{value:!0});var o=r(n),l=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),f=[0],h=function(e){function n(){i(this,n);var e=s(this,(n.__proto__||Object.getPrototypeOf(n)).call(this));return e.events_=null,e.listenerHandlers_=null,e.shouldUseFacade_=!1,e}return a(n,e),l(n,[{key:"addHandler_",value:function(e,t){return e?(Array.isArray(e)||(e=[e]),e.push(t)):e=t,e}},{key:"addListener",value:function(e,t,n){this.validateListener_(t);for(var r=this.toEventsArray_(e),i=0;i<r.length;i++)this.addSingleListener_(r[i],t,n);return new o["default"](this,e,t)}},{key:"addSingleListener_",value:function(e,t,n,r){this.runListenerHandlers_(e),(n||r)&&(t={"default":n,fn:t,origin:r}),this.events_=this.events_||{},this.events_[e]=this.addHandler_(this.events_[e],t)}},{key:"buildFacade_",value:function(e){if(this.getShouldUseFacade()){var t={preventDefault:function(){t.preventedDefault=!0},target:this,type:e};return t}}},{key:"disposeInternal",value:function(){this.events_=null}},{key:"emit",value:function(e){var n=this.getRawListeners_(e);if(0===n.length)return!1;var r=t.array.slice(arguments,1);return this.runListeners_(n,r,this.buildFacade_(e)),!0}},{key:"getRawListeners_",value:function(e){var t=u(this.events_&&this.events_[e]);return t.concat(u(this.events_&&this.events_["*"]))}},{key:"getShouldUseFacade",value:function(){return this.shouldUseFacade_}},{key:"listeners",value:function(e){return this.getRawListeners_(e).map(function(e){return e.fn?e.fn:e})}},{key:"many",value:function(e,t,n){for(var r=this.toEventsArray_(e),i=0;i<r.length;i++)this.many_(r[i],t,n);return new o["default"](this,e,n)}},{key:"many_",value:function(e,t,n){function r(){0===--t&&i.removeListener(e,r),n.apply(i,arguments)}var i=this;t<=0||i.addSingleListener_(e,r,!1,n)}},{key:"matchesListener_",value:function(e,t){var n=e.fn||e;return n===t||e.origin&&e.origin===t}},{key:"off",value:function(e,t){if(this.validateListener_(t),!this.events_)return this;for(var n=this.toEventsArray_(e),r=0;r<n.length;r++)this.events_[n[r]]=this.removeMatchingListenerObjs_(u(this.events_[n[r]]),t);return this}},{key:"on",value:function(){return this.addListener.apply(this,arguments)}},{key:"onListener",value:function(e){this.listenerHandlers_=this.addHandler_(this.listenerHandlers_,e)}},{key:"once",value:function(e,t){return this.many(e,1,t)}},{key:"removeAllListeners",value:function(e){if(this.events_)if(e)for(var t=this.toEventsArray_(e),n=0;n<t.length;n++)this.events_[t[n]]=null;else this.events_=null;return this}},{key:"removeMatchingListenerObjs_",value:function(e,t){for(var n=[],r=0;r<e.length;r++)this.matchesListener_(e[r],t)||n.push(e[r]);return n.length>0?n:null}},{key:"removeListener",value:function(){return this.off.apply(this,arguments)}},{key:"runListenerHandlers_",value:function(e){var t=this.listenerHandlers_;if(t){t=u(t);for(var n=0;n<t.length;n++)t[n](e)}}},{key:"runListeners_",value:function(e,t,n){n&&t.push(n);for(var r=[],i=0;i<e.length;i++){var s=e[i].fn||e[i];e[i]["default"]?r.push(s):s.apply(this,t)}if(!n||!n.preventedDefault)for(var a=0;a<r.length;a++)r[a].apply(this,t)}},{key:"setShouldUseFacade",value:function(e){return this.shouldUseFacade_=e,this}},{key:"toEventsArray_",value:function(e){return(0,t.isString)(e)&&(f[0]=e,e=f),e}},{key:"validateListener_",value:function(e){if(!(0,t.isFunction)(e))throw new TypeError("Listener must be a function")}}]),n}(t.Disposable);e["default"]=h});