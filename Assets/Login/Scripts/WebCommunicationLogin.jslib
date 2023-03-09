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
  
  JSSetAvatarURL: function (url) {
    ReactUnityWebGL.SetAvatarURL(Pointer_stringify(url));
  },
  
  JSSetAvatarImage: function () {
    ReactUnityWebGL.SetAvatarImage();
  },
  
  JSCopyToClipboard: function (textToCopy) {
    ReactUnityWebGL.CopyToClipboard(Pointer_stringify(textToCopy));
  },
  
  JSOnHubScene: function () {
      ReactUnityWebGL.OnHubScene();
    },
  
  
});

