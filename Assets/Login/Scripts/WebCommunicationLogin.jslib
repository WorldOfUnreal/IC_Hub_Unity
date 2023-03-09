mergeInto(LibraryManager.library, {
  // Create a new function with the same name as
  // the event listeners name and make sure the
  // parameters match as well.

  JSWalletsLogin: function (json) {
    ReactUnityWebGL.WalletsLogin(Pointer_stringify(json));
  },

  JSSetNameLogin: function (accountName) {
    ReactUnityWebGL.SetNameLogin(Pointer_stringify(accountName));
  },
  
  JSSetAvatar: function (url) {
    ReactUnityWebGL.SetAvatar(Pointer_stringify(url));
  },
  
  JSCopyToClipboard: function (textToCopy) {
    ReactUnityWebGL.CopyToClipboard(Pointer_stringify(textToCopy));
  },
  
  JSOnHubScene: function () {
      ReactUnityWebGL.OnHubScene();
    },
  
  
});

