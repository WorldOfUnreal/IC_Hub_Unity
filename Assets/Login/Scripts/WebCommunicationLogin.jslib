mergeInto(LibraryManager.library, {
  // Create a new function with the same name as
  // the event listeners name and make sure the
  // parameters match as well.

  JSWalletsLogin: function (walletID) {
    ReactUnityWebGL.WalletsLogin(Pointer_stringify(walletID));
  },

  JSSetNameLogin: function (accountName) {
    ReactUnityWebGL.SetNameLogin(Pointer_stringify(accountName));
  },
  
  JSSetAvatar: function () {
    ReactUnityWebGL.SetAvatar();
  },
  
  JSCopyToClipboard: function (textToCopy) {
    ReactUnityWebGL.CopyToClipboard(Pointer_stringify(textToCopy));
  },
  
});

