!function(t){function e(r){if(o[r])return o[r].exports;var n=o[r]={i:r,l:!1,exports:{}};return t[r].call(n.exports,n,n.exports,e),n.l=!0,n.exports}var o={};e.m=t,e.c=o,e.i=function(t){return t},e.d=function(t,o,r){e.o(t,o)||Object.defineProperty(t,o,{configurable:!1,enumerable:!0,get:r})},e.n=function(t){var o=t&&t.__esModule?function(){return t.default}:function(){return t};return e.d(o,"a",o),o},e.o=function(t,e){return Object.prototype.hasOwnProperty.call(t,e)},e.p="",e(e.s=21)}([function(t,e,o){var r=o(12),n=r.Symbol;t.exports=n},function(t,e,o){function r(){var t=gapi.client.request({path:s}).then(function(t){return t});return new t.constructor(function(e,o){var r=[];t.then(function t(n){var c=n.result;c.items?r=r.concat(c.items):o(new Error("You do not have any Google Analytics accounts. Go to http://google.com/analytics to sign up.")),c.startIndex+c.itemsPerPage<=c.totalResults?gapi.client.request({path:s,params:{"start-index":c.startIndex+c.itemsPerPage}}).then(t):e(new i(r))}).then(null,o)})}var n,i=o(3),s="/analytics/v3/management/accountSummaries";t.exports={get:function(t){return t&&(n=null),n||(n=r())}}},function(t,e,o){"use strict";function r(t,e){if(!(t instanceof e))throw new TypeError("Cannot call a class as a function")}function n(t,e){if(!t)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!e||"object"!=typeof e&&"function"!=typeof e?t:e}function i(t,e){if("function"!=typeof e&&null!==e)throw new TypeError("Super expression must either be null or a function, not "+typeof e);t.prototype=Object.create(e&&e.prototype,{constructor:{value:t,enumerable:!1,writable:!0,configurable:!0}}),e&&(Object.setPrototypeOf?Object.setPrototypeOf(t,e):t.__proto__=e)}o.d(e,"a",function(){return p});var s=o(13),c=o.n(s),u=function(t){return null==t||""===t},p=function(t){for(var e=arguments.length,o=Array(e>1?e-1:0),r=1;r<e;r++)o[r-1]=arguments[r];var n=[];return t.forEach(function(t,e){var r=o[e];u(t)||n.push(t),u(r)||n.push(c()(r))}),n.join("")};!function(t){function e(t){var o=t.title,i=t.message;r(this,e);var s=n(this,(e.__proto__||Object.getPrototypeOf(e)).call(this,i));return s.title=o,s}i(e,t)}(Error)},function(t,e){function o(t){this.accounts_=t,this.webProperties_=[],this.profiles_=[],this.accountsById_={},this.webPropertiesById_=this.propertiesById_={},this.profilesById_=this.viewsById_={};for(var e,o=0;e=this.accounts_[o];o++)if(this.accountsById_[e.id]={self:e},e.webProperties){r(e,"webProperties","properties");for(var n,i=0;n=e.webProperties[i];i++)if(this.webProperties_.push(n),this.webPropertiesById_[n.id]={self:n,parent:e},n.profiles){r(n,"profiles","views");for(var s,c=0;s=n.profiles[c];c++)this.profiles_.push(s),this.profilesById_[s.id]={self:s,parent:n,grandParent:e}}}}function r(t,e,o){Object.defineProperty?Object.defineProperty(t,o,{get:function(){return t[e]}}):t[o]=t[e]}o.prototype.all=function(){return this.accounts_},r(o.prototype,"all","allAccounts"),o.prototype.allWebProperties=function(){return this.webProperties_},r(o.prototype,"allWebProperties","allProperties"),o.prototype.allProfiles=function(){return this.profiles_},r(o.prototype,"allProfiles","allViews"),o.prototype.get=function(t){if(!!t.accountId+!!t.webPropertyId+!!t.propertyId+!!t.profileId+!!t.viewId>1)throw new Error('get() only accepts an object with a single property: either "accountId", "webPropertyId", "propertyId", "profileId" or "viewId"');return this.getProfile(t.profileId||t.viewId)||this.getWebProperty(t.webPropertyId||t.propertyId)||this.getAccount(t.accountId)},o.prototype.getAccount=function(t){return this.accountsById_[t]&&this.accountsById_[t].self},o.prototype.getWebProperty=function(t){return this.webPropertiesById_[t]&&this.webPropertiesById_[t].self},r(o.prototype,"getWebProperty","getProperty"),o.prototype.getProfile=function(t){return this.profilesById_[t]&&this.profilesById_[t].self},r(o.prototype,"getProfile","getView"),o.prototype.getAccountByProfileId=function(t){return this.profilesById_[t]&&this.profilesById_[t].grandParent},r(o.prototype,"getAccountByProfileId","getAccountByViewId"),o.prototype.getWebPropertyByProfileId=function(t){return this.profilesById_[t]&&this.profilesById_[t].parent},r(o.prototype,"getWebPropertyByProfileId","getPropertyByViewId"),o.prototype.getAccountByWebPropertyId=function(t){return this.webPropertiesById_[t]&&this.webPropertiesById_[t].parent},r(o.prototype,"getAccountByWebPropertyId","getAccountByPropertyId"),t.exports=o},function(t,e){function o(t,e){for(var o=-1,r=null==t?0:t.length,n=Array(r);++o<r;)n[o]=e(t[o],o,t);return n}t.exports=o},function(t,e,o){function r(t){return null==t?void 0===t?u:c:p&&p in Object(t)?i(t):s(t)}var n=o(0),i=o(10),s=o(11),c="[object Null]",u="[object Undefined]",p=n?n.toStringTag:void 0;t.exports=r},function(t,e){function o(t){return function(e){return null==t?void 0:t[e]}}t.exports=o},function(t,e,o){function r(t){if("string"==typeof t)return t;if(s(t))return i(t,r)+"";if(c(t))return a?a.call(t):"";var e=t+"";return"0"==e&&1/t==-u?"-0":e}var n=o(0),i=o(4),s=o(14),c=o(16),u=1/0,p=n?n.prototype:void 0,a=p?p.toString:void 0;t.exports=r},function(t,e,o){var r=o(6),n={"&":"&amp;","<":"&lt;",">":"&gt;",'"':"&quot;","'":"&#39;"},i=r(n);t.exports=i},function(t,e,o){(function(e){var o="function"==typeof Symbol&&"symbol"==typeof Symbol.iterator?function(t){return typeof t}:function(t){return t&&"function"==typeof Symbol&&t.constructor===Symbol&&t!==Symbol.prototype?"symbol":typeof t},r="object"==(void 0===e?"undefined":o(e))&&e&&e.Object===Object&&e;t.exports=r}).call(e,o(18))},function(t,e,o){function r(t){var e=s.call(t,u),o=t[u];try{t[u]=void 0;var r=!0}catch(t){}var n=c.call(t);return r&&(e?t[u]=o:delete t[u]),n}var n=o(0),i=Object.prototype,s=i.hasOwnProperty,c=i.toString,u=n?n.toStringTag:void 0;t.exports=r},function(t,e){function o(t){return n.call(t)}var r=Object.prototype,n=r.toString;t.exports=o},function(t,e,o){var r="function"==typeof Symbol&&"symbol"==typeof Symbol.iterator?function(t){return typeof t}:function(t){return t&&"function"==typeof Symbol&&t.constructor===Symbol&&t!==Symbol.prototype?"symbol":typeof t},n=o(9),i="object"==("undefined"==typeof self?"undefined":r(self))&&self&&self.Object===Object&&self,s=n||i||Function("return this")();t.exports=s},function(t,e,o){function r(t){return t=i(t),t&&c.test(t)?t.replace(s,n):t}var n=o(8),i=o(17),s=/[&<>"']/g,c=RegExp(s.source);t.exports=r},function(t,e){var o=Array.isArray;t.exports=o},function(t,e){function o(t){return null!=t&&"object"==(void 0===t?"undefined":r(t))}var r="function"==typeof Symbol&&"symbol"==typeof Symbol.iterator?function(t){return typeof t}:function(t){return t&&"function"==typeof Symbol&&t.constructor===Symbol&&t!==Symbol.prototype?"symbol":typeof t};t.exports=o},function(t,e,o){function r(t){return"symbol"==(void 0===t?"undefined":n(t))||s(t)&&i(t)==c}var n="function"==typeof Symbol&&"symbol"==typeof Symbol.iterator?function(t){return typeof t}:function(t){return t&&"function"==typeof Symbol&&t.constructor===Symbol&&t!==Symbol.prototype?"symbol":typeof t},i=o(5),s=o(15),c="[object Symbol]";t.exports=r},function(t,e,o){function r(t){return null==t?"":n(t)}var n=o(7);t.exports=r},function(t,e){var o,r="function"==typeof Symbol&&"symbol"==typeof Symbol.iterator?function(t){return typeof t}:function(t){return t&&"function"==typeof Symbol&&t.constructor===Symbol&&t!==Symbol.prototype?"symbol":typeof t};o=function(){return this}();try{o=o||Function("return this")()||(0,eval)("this")}catch(t){"object"===("undefined"==typeof window?"undefined":r(window))&&(o=window)}t.exports=o},,,function(t,e,o){"use strict";Object.defineProperty(e,"__esModule",{value:!0});var r=o(1),n=o.n(r),i=o(2),s=function(t,e){return Object.freeze(Object.defineProperties(t,{raw:{value:Object.freeze(e)}}))}(["<option ",' value="','">\n        ',"\n      </option>"],["<option ",' value="','">\n        ',"\n      </option>"]);gapi.analytics.ready(function(){function t(t,e,r){t.innerHTML=e.map(function(t){var e=t.id,n=t.name;return o.i(i.a)(s,e==r?"selected":"",e,n)}).join("")}function e(t){return t.ids||t.viewId?{prop:"viewId",value:t.viewId||t.ids&&t.ids.replace(/^ga:/,"")}:t.propertyId?{prop:"propertyId",value:t.propertyId}:t.accountId?{prop:"accountId",value:t.accountId}:void 0}gapi.analytics.createComponent("ViewSelector2",{execute:function(){return this.setup_(function(){this.updateAccounts_(),this.changed_&&(this.render_(),this.onChange_())}.bind(this)),this},set:function(t){if(!!t.ids+!!t.viewId+!!t.propertyId+!!t.accountId>1)throw new Error('You cannot specify more than one of the following options: "ids", "viewId", "accountId", "propertyId"');if(t.container&&this.container)throw new Error("You cannot change containers once a view selector has been rendered on the page.");var e=this.get();return e.ids==t.ids&&e.viewId==t.viewId&&e.propertyId==t.propertyId&&e.accountId==t.accountId||(e.ids=null,e.viewId=null,e.propertyId=null,e.accountId=null),gapi.analytics.Component.prototype.set.call(this,t)},setup_:function(t){var e=this,o=function(){n.a.get().then(function(o){e.summaries=o,e.accounts=e.summaries.all(),t()},function(t){e.emit("error",t)})};gapi.analytics.auth.isAuthorized()?o():gapi.analytics.auth.on("signIn",o)},updateAccounts_:function(){var t=this.get(),o=e(t),r=void 0,n=void 0,i=void 0;if(!this.summaries.all().length)return void this.emit("error",new Error('This user does not have any Google Analytics accounts. You can sign up at "www.google.com/analytics".'));if(o)switch(o.prop){case"viewId":r=this.summaries.getProfile(o.value),n=this.summaries.getAccountByProfileId(o.value),i=this.summaries.getWebPropertyByProfileId(o.value);break;case"propertyId":i=this.summaries.getWebProperty(o.value),n=this.summaries.getAccountByWebPropertyId(o.value),r=i&&i.views&&i.views[0];break;case"accountId":n=this.summaries.getAccount(o.value),i=n&&n.properties&&n.properties[0],r=i&&i.views&&i.views[0]}else n=this.accounts[0],i=n&&n.properties&&n.properties[0],r=i&&i.views&&i.views[0];n||i||r?n==this.account&&i==this.property&&r==this.view||(this.changed_={account:n&&n!=this.account,property:i&&i!=this.property,view:r&&r!=this.view},this.account=n,this.properties=n.properties,this.property=i,this.views=i&&i.views,this.view=r,this.ids=r&&"ga:"+r.id):this.emit("error",new Error("This user does not have access to "+o.prop.slice(0,-2)+" : "+o.value))},render_:function(){var e=this.get();this.container="string"==typeof e.container?document.getElementById(e.container):e.container,this.container.innerHTML=e.template||this.template;var o=this.container.querySelectorAll("select"),r=this.accounts,n=this.properties||[{name:"(Empty)",id:""}],i=this.views||[{name:"(Empty)",id:""}];t(o[0],r,this.account.id),t(o[1],n,this.property&&this.property.id),t(o[2],i,this.view&&this.view.id),o[0].onchange=this.onUserSelect_.bind(this,o[0],"accountId"),o[1].onchange=this.onUserSelect_.bind(this,o[1],"propertyId"),o[2].onchange=this.onUserSelect_.bind(this,o[2],"viewId")},onChange_:function(){var t={account:this.account,property:this.property,view:this.view,ids:this.view&&"ga:"+this.view.id};this.changed_&&(this.changed_.account&&this.emit("accountChange",t),this.changed_.property&&this.emit("propertyChange",t),this.changed_.view&&(this.emit("viewChange",t),this.emit("idsChange",t),this.emit("change",t.ids))),this.changed_=null},onUserSelect_:function(t,e){var o={};o[e]=t.value,this.set(o),this.execute()},template:'<div class="ViewSelector2">  <div class="ViewSelector2-item">    <label>Account</label>    <select class="FormField"></select>  </div>  <div class="ViewSelector2-item">    <label>Property</label>    <select class="FormField"></select>  </div>  <div class="ViewSelector2-item">    <label>View</label>    <select class="FormField"></select>  </div></div>'})})}]);