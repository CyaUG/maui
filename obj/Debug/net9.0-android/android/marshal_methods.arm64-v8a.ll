; ModuleID = 'marshal_methods.arm64-v8a.ll'
source_filename = "marshal_methods.arm64-v8a.ll"
target datalayout = "e-m:e-i8:8:32-i16:16:32-i64:64-i128:128-n32:64-S128"
target triple = "aarch64-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [371 x ptr] zeroinitializer, align 8

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [1113 x i64] [
	i64 u0x001e58127c546039, ; 0: lib_System.Globalization.dll.so => 42
	i64 u0x0024d0f62dee05bd, ; 1: Xamarin.KotlinX.Coroutines.Core.dll => 328
	i64 u0x0071cf2d27b7d61e, ; 2: lib_Xamarin.AndroidX.SwipeRefreshLayout.dll.so => 301
	i64 u0x00b2f568250923fe, ; 3: PInvoke.Kernel32.dll => 206
	i64 u0x01109b0e4d99e61f, ; 4: System.ComponentModel.Annotations.dll => 13
	i64 u0x012194911580305f, ; 5: lib_ExoPlayer.UI.dll.so => 229
	i64 u0x01af0bd6467d518e, ; 6: lib_ZXing.Net.MAUI.dll.so => 331
	i64 u0x02123411c4e01926, ; 7: lib_Xamarin.AndroidX.Navigation.Runtime.dll.so => 290
	i64 u0x022e81ea9c46e03a, ; 8: lib_CommunityToolkit.Maui.Core.dll.so => 177
	i64 u0x02787f0f071f1eb7, ; 9: Youth => 0
	i64 u0x0284512fad379f7e, ; 10: System.Runtime.Handles => 105
	i64 u0x02abedc11addc1ed, ; 11: lib_Mono.Android.Runtime.dll.so => 171
	i64 u0x02f55bf70672f5c8, ; 12: lib_System.IO.FileSystem.DriveInfo.dll.so => 48
	i64 u0x032267b2a94db371, ; 13: lib_Xamarin.AndroidX.AppCompat.dll.so => 239
	i64 u0x03621c804933a890, ; 14: System.Buffers => 7
	i64 u0x0399610510a38a38, ; 15: lib_System.Private.DataContractSerialization.dll.so => 86
	i64 u0x044440a55165631e, ; 16: lib-cs-Microsoft.Maui.Controls.resources.dll.so => 335
	i64 u0x0470607fd33c32db, ; 17: Microsoft.IdentityModel.Abstractions.dll => 191
	i64 u0x047408741db2431a, ; 18: Xamarin.AndroidX.DynamicAnimation => 263
	i64 u0x04c74ef0f9b1afa5, ; 19: ExoPlayer.Hls => 224
	i64 u0x0517ef04e06e9f76, ; 20: System.Net.Primitives => 71
	i64 u0x051a3be159e4ef99, ; 21: Xamarin.GooglePlayServices.Tasks => 322
	i64 u0x0565d18c6da3de38, ; 22: Xamarin.AndroidX.RecyclerView => 294
	i64 u0x0581db89237110e9, ; 23: lib_System.Collections.dll.so => 12
	i64 u0x05989cb940b225a9, ; 24: Microsoft.Maui.dll => 199
	i64 u0x05a1c25e78e22d87, ; 25: lib_System.Runtime.CompilerServices.Unsafe.dll.so => 102
	i64 u0x062f25022662a72b, ; 26: PInvoke.Kernel32 => 206
	i64 u0x06388ffe9f6c161a, ; 27: System.Xml.Linq.dll => 156
	i64 u0x06600c4c124cb358, ; 28: System.Configuration.dll => 19
	i64 u0x067f95c5ddab55b3, ; 29: lib_Xamarin.AndroidX.Fragment.Ktx.dll.so => 268
	i64 u0x0680a433c781bb3d, ; 30: Xamarin.AndroidX.Collection.Jvm => 250
	i64 u0x069fff96ec92a91d, ; 31: System.Xml.XPath.dll => 161
	i64 u0x070b0847e18dab68, ; 32: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 265
	i64 u0x0739448d84d3b016, ; 33: lib_Xamarin.AndroidX.VectorDrawable.dll.so => 304
	i64 u0x07469f2eecce9e85, ; 34: mscorlib.dll => 167
	i64 u0x07adf62cd01e889a, ; 35: ExoPlayer.Common => 216
	i64 u0x07c57877c7ba78ad, ; 36: ru/Microsoft.Maui.Controls.resources.dll => 357
	i64 u0x07dcdc7460a0c5e4, ; 37: System.Collections.NonGeneric => 10
	i64 u0x08122e52765333c8, ; 38: lib_Microsoft.Extensions.Logging.Debug.dll.so => 188
	i64 u0x088610fc2509f69e, ; 39: lib_Xamarin.AndroidX.VectorDrawable.Animated.dll.so => 305
	i64 u0x08a7c865576bbde7, ; 40: System.Reflection.Primitives => 96
	i64 u0x08c9d051a4a817e5, ; 41: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 260
	i64 u0x08f3c9788ee2153c, ; 42: Xamarin.AndroidX.DrawerLayout => 262
	i64 u0x09138715c92dba90, ; 43: lib_System.ComponentModel.Annotations.dll.so => 13
	i64 u0x0919c28b89381a0b, ; 44: lib_Microsoft.Extensions.Options.dll.so => 189
	i64 u0x092266563089ae3e, ; 45: lib_System.Collections.NonGeneric.dll.so => 10
	i64 u0x098b50f911ccea8d, ; 46: lib_Xamarin.GooglePlayServices.Basement.dll.so => 320
	i64 u0x09d144a7e214d457, ; 47: System.Security.Cryptography => 127
	i64 u0x09e2b9f743db21a8, ; 48: lib_System.Reflection.Metadata.dll.so => 95
	i64 u0x0abb3e2b271edc45, ; 49: System.Threading.Channels.dll => 140
	i64 u0x0ac76ad34327b01e, ; 50: ExoPlayer.Core => 218
	i64 u0x0b06b1feab070143, ; 51: System.Formats.Tar => 39
	i64 u0x0be2e1f8ce4064ed, ; 52: Xamarin.AndroidX.ViewPager => 307
	i64 u0x0c3d7adcdb333bf0, ; 53: Xamarin.AndroidX.Camera.Lifecycle => 246
	i64 u0x0c3dd9438f54f672, ; 54: lib_Xamarin.GooglePlayServices.Maps.dll.so => 321
	i64 u0x0c59ad9fbbd43abe, ; 55: Mono.Android => 172
	i64 u0x0c65741e86371ee3, ; 56: lib_Xamarin.Android.Glide.GifDecoder.dll.so => 233
	i64 u0x0c74af560004e816, ; 57: Microsoft.Win32.Registry.dll => 5
	i64 u0x0c7790f60165fc06, ; 58: lib_Microsoft.Maui.Essentials.dll.so => 200
	i64 u0x0c83c82812e96127, ; 59: lib_System.Net.Mail.dll.so => 67
	i64 u0x0cce4bce83380b7f, ; 60: Xamarin.AndroidX.Security.SecurityCrypto => 298
	i64 u0x0d13cd7cce4284e4, ; 61: System.Security.SecureString => 130
	i64 u0x0d63f4f73521c24f, ; 62: lib_Xamarin.AndroidX.SavedState.SavedState.Ktx.dll.so => 297
	i64 u0x0daae0a9bef5dcf3, ; 63: zh-Hant\Microsoft.Maui.Controls.resources => 366
	i64 u0x0e04e702012f8463, ; 64: Xamarin.AndroidX.Emoji2 => 264
	i64 u0x0e14e73a54dda68e, ; 65: lib_System.Net.NameResolution.dll.so => 68
	i64 u0x0f37dd7a62ae99af, ; 66: lib_Xamarin.AndroidX.Collection.Ktx.dll.so => 251
	i64 u0x0f598803ed2e44b2, ; 67: ExoPlayer.DataSource => 221
	i64 u0x0f5e7abaa7cf470a, ; 68: System.Net.HttpListener => 66
	i64 u0x1001f97bbe242e64, ; 69: System.IO.UnmanagedMemoryStream => 57
	i64 u0x102a31b45304b1da, ; 70: Xamarin.AndroidX.CustomView => 259
	i64 u0x1065c4cb554c3d75, ; 71: System.IO.IsolatedStorage.dll => 52
	i64 u0x107031f59b49e23d, ; 72: hr\Microsoft.Maui.Controls.resources => 344
	i64 u0x10f6cfcbcf801616, ; 73: System.IO.Compression.Brotli => 43
	i64 u0x114443cdcf2091f1, ; 74: System.Security.Cryptography.Primitives => 125
	i64 u0x118d570f508803d1, ; 75: Xamarin.AndroidX.Camera.Camera2.dll => 244
	i64 u0x11a603952763e1d4, ; 76: System.Net.Mail => 67
	i64 u0x11a70d0e1009fb11, ; 77: System.Net.WebSockets.dll => 81
	i64 u0x11f26371eee0d3c1, ; 78: lib_Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll.so => 279
	i64 u0x11fbe62d469cc1c8, ; 79: Microsoft.VisualStudio.DesignTools.TapContract.dll => 369
	i64 u0x12128b3f59302d47, ; 80: lib_System.Xml.Serialization.dll.so => 158
	i64 u0x123639456fb056da, ; 81: System.Reflection.Emit.Lightweight.dll => 92
	i64 u0x12521e9764603eaa, ; 82: lib_System.Resources.Reader.dll.so => 99
	i64 u0x125b7f94acb989db, ; 83: Xamarin.AndroidX.RecyclerView.dll => 294
	i64 u0x12d3b63863d4ab0b, ; 84: lib_System.Threading.Overlapped.dll.so => 141
	i64 u0x12d72d80cde566b1, ; 85: ExoPlayer.SmoothStreaming => 227
	i64 u0x134eab1061c395ee, ; 86: System.Transactions => 151
	i64 u0x138567fa954faa55, ; 87: Xamarin.AndroidX.Browser => 243
	i64 u0x13a01de0cbc3f06c, ; 88: lib-fr-Microsoft.Maui.Controls.resources.dll.so => 341
	i64 u0x13beedefb0e28a45, ; 89: lib_System.Xml.XmlDocument.dll.so => 162
	i64 u0x13f1e5e209e91af4, ; 90: lib_Java.Interop.dll.so => 169
	i64 u0x143d8ea60a6a4011, ; 91: Microsoft.Extensions.DependencyInjection.Abstractions => 185
	i64 u0x1497051b917530bd, ; 92: lib_System.Net.WebSockets.dll.so => 81
	i64 u0x14b78ce3adce0011, ; 93: Microsoft.VisualStudio.DesignTools.TapContract => 369
	i64 u0x14e68447938213b7, ; 94: Xamarin.AndroidX.Collection.Ktx.dll => 251
	i64 u0x152a448bd1e745a7, ; 95: Microsoft.Win32.Primitives => 4
	i64 u0x1557de0138c445f4, ; 96: lib_Microsoft.Win32.Registry.dll.so => 5
	i64 u0x15bdc156ed462f2f, ; 97: lib_System.IO.FileSystem.dll.so => 51
	i64 u0x15e300c2c1668655, ; 98: System.Resources.Writer.dll => 101
	i64 u0x15e440881244515c, ; 99: ExoPlayer.Core.dll => 218
	i64 u0x16550c63c9be4f5e, ; 100: vi\Microsoft.Maui.Controls.resources => 363
	i64 u0x1688bf9c0b89fc8a, ; 101: ExoPlayer.Common.dll => 216
	i64 u0x16b49cb47b483477, ; 102: CommunityToolkit.Common => 174
	i64 u0x16bf2a22df043a09, ; 103: System.IO.Pipes.dll => 56
	i64 u0x16cd84ceca3b17f3, ; 104: lib_BouncyCastle.Crypto.dll.so => 210
	i64 u0x16ea2b318ad2d830, ; 105: System.Security.Cryptography.Algorithms => 120
	i64 u0x16eeae54c7ebcc08, ; 106: System.Reflection.dll => 98
	i64 u0x17125c9a85b4929f, ; 107: lib_netstandard.dll.so => 168
	i64 u0x1716866f7416792e, ; 108: lib_System.Security.AccessControl.dll.so => 118
	i64 u0x17414f427ab6be84, ; 109: sv\Microsoft.Maui.Controls.resources => 359
	i64 u0x174f71c46216e44a, ; 110: Xamarin.KotlinX.Coroutines.Core => 328
	i64 u0x1752c12f1e1fc00c, ; 111: System.Core => 21
	i64 u0x17b56e25558a5d36, ; 112: lib-hu-Microsoft.Maui.Controls.resources.dll.so => 345
	i64 u0x17f9358913beb16a, ; 113: System.Text.Encodings.Web => 137
	i64 u0x1809fb23f29ba44a, ; 114: lib_System.Reflection.TypeExtensions.dll.so => 97
	i64 u0x18a9befae51bb361, ; 115: System.Net.WebClient => 77
	i64 u0x18f0ce884e87d89a, ; 116: nb/Microsoft.Maui.Controls.resources.dll => 351
	i64 u0x193d7a04b7eda8bc, ; 117: lib_Xamarin.AndroidX.Print.dll.so => 292
	i64 u0x19777fba3c41b398, ; 118: Xamarin.AndroidX.Startup.StartupRuntime.dll => 300
	i64 u0x19a4c090f14ebb66, ; 119: System.Security.Claims => 119
	i64 u0x19f0c7f9af0f1ef9, ; 120: PInvoke.User32 => 207
	i64 u0x1a040febb58bf51e, ; 121: lib_Xamarin.AndroidX.Camera.View.dll.so => 247
	i64 u0x1a91866a319e9259, ; 122: lib_System.Collections.Concurrent.dll.so => 8
	i64 u0x1aac34d1917ba5d3, ; 123: lib_System.dll.so => 165
	i64 u0x1aad60783ffa3e5b, ; 124: lib-th-Microsoft.Maui.Controls.resources.dll.so => 360
	i64 u0x1aea8f1c3b282172, ; 125: lib_System.Net.Ping.dll.so => 70
	i64 u0x1b4b7a1d0d265fa2, ; 126: Xamarin.Android.Glide.DiskLruCache => 232
	i64 u0x1bbdb16cfa73e785, ; 127: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.Android => 280
	i64 u0x1bc766e07b2b4241, ; 128: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 295
	i64 u0x1c753b5ff15bce1b, ; 129: Mono.Android.Runtime.dll => 171
	i64 u0x1cd47467799d8250, ; 130: System.Threading.Tasks.dll => 145
	i64 u0x1d23eafdc6dc346c, ; 131: System.Globalization.Calendars.dll => 40
	i64 u0x1d4c109ca6e27ed8, ; 132: lib_Microsoft.Maui.Controls.Compatibility.dll.so => 195
	i64 u0x1da4110562816681, ; 133: Xamarin.AndroidX.Security.SecurityCrypto.dll => 298
	i64 u0x1db6820994506bf5, ; 134: System.IO.FileSystem.AccessControl.dll => 47
	i64 u0x1dbb0c2c6a999acb, ; 135: System.Diagnostics.StackTrace => 30
	i64 u0x1dcda680b17dc5bb, ; 136: lib_Xamarin.Google.Guava.FailureAccess.dll.so => 317
	i64 u0x1e31d75d2b92d80d, ; 137: ExoPlayer.dll => 215
	i64 u0x1e3d87657e9659bc, ; 138: Xamarin.AndroidX.Navigation.UI => 291
	i64 u0x1e71143913d56c10, ; 139: lib-ko-Microsoft.Maui.Controls.resources.dll.so => 349
	i64 u0x1e7c31185e2fb266, ; 140: lib_System.Threading.Tasks.Parallel.dll.so => 144
	i64 u0x1ed8fcce5e9b50a0, ; 141: Microsoft.Extensions.Options.dll => 189
	i64 u0x1f055d15d807e1b2, ; 142: System.Xml.XmlSerializer => 163
	i64 u0x1f1ed22c1085f044, ; 143: lib_System.Diagnostics.FileVersionInfo.dll.so => 28
	i64 u0x1f61df9c5b94d2c1, ; 144: lib_System.Numerics.dll.so => 84
	i64 u0x1f750bb5421397de, ; 145: lib_Xamarin.AndroidX.Tracing.Tracing.dll.so => 302
	i64 u0x20237ea48006d7a8, ; 146: lib_System.Net.WebClient.dll.so => 77
	i64 u0x209375905fcc1bad, ; 147: lib_System.IO.Compression.Brotli.dll.so => 43
	i64 u0x20a6f00ff6704fd6, ; 148: ExoPlayer.Transformer => 228
	i64 u0x20aefaf77b742b27, ; 149: ExoPlayer.Dash => 219
	i64 u0x20fab3cf2dfbc8df, ; 150: lib_System.Diagnostics.Process.dll.so => 29
	i64 u0x2110167c128cba15, ; 151: System.Globalization => 42
	i64 u0x21419508838f7547, ; 152: System.Runtime.CompilerServices.VisualC => 103
	i64 u0x2174319c0d835bc9, ; 153: System.Runtime => 117
	i64 u0x2198e5bc8b7153fa, ; 154: Xamarin.AndroidX.Annotation.Experimental.dll => 237
	i64 u0x219ea1b751a4dee4, ; 155: lib_System.IO.Compression.ZipFile.dll.so => 45
	i64 u0x21cc7e445dcd5469, ; 156: System.Reflection.Emit.ILGeneration => 91
	i64 u0x224538d85ed15a82, ; 157: System.IO.Pipes => 56
	i64 u0x22908438c6bed1af, ; 158: lib_System.Threading.Timer.dll.so => 148
	i64 u0x22fbc14e981e3b45, ; 159: lib_Microsoft.VisualStudio.DesignTools.MobileTapContracts.dll.so => 368
	i64 u0x2347c268e3e4e536, ; 160: Xamarin.GooglePlayServices.Basement.dll => 320
	i64 u0x237be844f1f812c7, ; 161: System.Threading.Thread.dll => 146
	i64 u0x23852b3bdc9f7096, ; 162: System.Resources.ResourceManager => 100
	i64 u0x23986dd7e5d4fc01, ; 163: System.IO.FileSystem.Primitives.dll => 49
	i64 u0x23cc21e24bf661d5, ; 164: ExoPlayer.SmoothStreaming.dll => 227
	i64 u0x2407aef2bbe8fadf, ; 165: System.Console => 20
	i64 u0x240abe014b27e7d3, ; 166: Xamarin.AndroidX.Core.dll => 256
	i64 u0x247619fe4413f8bf, ; 167: System.Runtime.Serialization.Primitives.dll => 114
	i64 u0x24de8d301281575e, ; 168: Xamarin.Android.Glide => 230
	i64 u0x24e51ef140650238, ; 169: MvvmHelpers => 212
	i64 u0x252073cc3caa62c2, ; 170: fr/Microsoft.Maui.Controls.resources.dll => 341
	i64 u0x256b8d41255f01b1, ; 171: Xamarin.Google.Crypto.Tink.Android => 314
	i64 u0x25e1850d10cdc8f7, ; 172: lib_Xamarin.AndroidX.Camera.Camera2.dll.so => 244
	i64 u0x2662c629b96b0b30, ; 173: lib_Xamarin.Kotlin.StdLib.dll.so => 324
	i64 u0x268c1439f13bcc29, ; 174: lib_Microsoft.Extensions.Primitives.dll.so => 190
	i64 u0x268f1dca6d06d437, ; 175: Xamarin.AndroidX.Camera.Core => 245
	i64 u0x26a670e154a9c54b, ; 176: System.Reflection.Extensions.dll => 94
	i64 u0x26d077d9678fe34f, ; 177: System.IO.dll => 58
	i64 u0x270a44600c921861, ; 178: System.IdentityModel.Tokens.Jwt => 213
	i64 u0x273f3515de5faf0d, ; 179: id/Microsoft.Maui.Controls.resources.dll => 346
	i64 u0x2749e87accaaeaad, ; 180: tr\Microsoft.Maui.Controls.resources => 361
	i64 u0x2759af78ab94d39b, ; 181: System.Net.WebSockets => 81
	i64 u0x2799af53798eaf62, ; 182: lib_Validation.dll.so => 214
	i64 u0x27b2b16f3e9de038, ; 183: Xamarin.Google.Crypto.Tink.Android.dll => 314
	i64 u0x27b410442fad6cf1, ; 184: Java.Interop.dll => 169
	i64 u0x27b97e0d52c3034a, ; 185: System.Diagnostics.Debug => 26
	i64 u0x27d2e9e91761b63b, ; 186: zh-HK\Microsoft.Maui.Controls.resources => 364
	i64 u0x2801845a2c71fbfb, ; 187: System.Net.Primitives.dll => 71
	i64 u0x286835e259162700, ; 188: lib_Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll.so => 293
	i64 u0x2949f3617a02c6b2, ; 189: Xamarin.AndroidX.ExifInterface => 266
	i64 u0x29f947844fb7fc11, ; 190: Microsoft.Maui.Controls.HotReload.Forms => 367
	i64 u0x2a128783efe70ba0, ; 191: uk/Microsoft.Maui.Controls.resources.dll => 362
	i64 u0x2a3b095612184159, ; 192: lib_System.Net.NetworkInformation.dll.so => 69
	i64 u0x2a6507a5ffabdf28, ; 193: System.Diagnostics.TraceSource.dll => 33
	i64 u0x2ad5d6b13b7a3e04, ; 194: System.ComponentModel.DataAnnotations.dll => 14
	i64 u0x2af298f63581d886, ; 195: System.Text.RegularExpressions.dll => 139
	i64 u0x2af615542f04da50, ; 196: System.IdentityModel.Tokens.Jwt.dll => 213
	i64 u0x2afc1c4f898552ee, ; 197: lib_System.Formats.Asn1.dll.so => 38
	i64 u0x2b1258a9f7c8559b, ; 198: CommunityToolkit.Diagnostics => 175
	i64 u0x2b148910ed40fbf9, ; 199: zh-Hant/Microsoft.Maui.Controls.resources.dll => 366
	i64 u0x2b6989d78cba9a15, ; 200: Xamarin.AndroidX.Concurrent.Futures.dll => 252
	i64 u0x2c8bd14bb93a7d82, ; 201: lib-pl-Microsoft.Maui.Controls.resources.dll.so => 353
	i64 u0x2cbd9262ca785540, ; 202: lib_System.Text.Encoding.CodePages.dll.so => 134
	i64 u0x2cc9e1fed6257257, ; 203: lib_System.Reflection.Emit.Lightweight.dll.so => 92
	i64 u0x2cd723e9fe623c7c, ; 204: lib_System.Private.Xml.Linq.dll.so => 88
	i64 u0x2ce03196fe1170d2, ; 205: Microsoft.Maui.Controls.Maps.dll => 197
	i64 u0x2d169d318a968379, ; 206: System.Threading.dll => 149
	i64 u0x2d1d1413dd10c597, ; 207: Xamarin.Google.Guava.FailureAccess => 317
	i64 u0x2d47774b7d993f59, ; 208: sv/Microsoft.Maui.Controls.resources.dll => 359
	i64 u0x2d5ffcae1ad0aaca, ; 209: System.Data.dll => 24
	i64 u0x2db915caf23548d2, ; 210: System.Text.Json.dll => 138
	i64 u0x2dcaa0bb15a4117a, ; 211: System.IO.UnmanagedMemoryStream.dll => 57
	i64 u0x2e5a40c319acb800, ; 212: System.IO.FileSystem => 51
	i64 u0x2e6f1f226821322a, ; 213: el/Microsoft.Maui.Controls.resources.dll => 338
	i64 u0x2eec9f3bcd5e1f36, ; 214: de\Microsoft.Maui.Controls.resources => 337
	i64 u0x2f02f94df3200fe5, ; 215: System.Diagnostics.Process => 29
	i64 u0x2f2e98e1c89b1aff, ; 216: System.Xml.ReaderWriter => 157
	i64 u0x2f5911d9ba814e4e, ; 217: System.Diagnostics.Tracing => 34
	i64 u0x2f84070a459bc31f, ; 218: lib_System.Xml.dll.so => 164
	i64 u0x309ee9eeec09a71e, ; 219: lib_Xamarin.AndroidX.Fragment.dll.so => 267
	i64 u0x309f2bedefa9a318, ; 220: Microsoft.IdentityModel.Abstractions => 191
	i64 u0x30bde19041cd89dd, ; 221: lib_Microsoft.Maui.Maps.dll.so => 202
	i64 u0x30c6dda129408828, ; 222: System.IO.IsolatedStorage => 52
	i64 u0x31195fef5d8fb552, ; 223: _Microsoft.Android.Resource.Designer.dll => 370
	i64 u0x312c8ed623cbfc8d, ; 224: Xamarin.AndroidX.Window.dll => 309
	i64 u0x31496b779ed0663d, ; 225: lib_System.Reflection.DispatchProxy.dll.so => 90
	i64 u0x32243413e774362a, ; 226: Xamarin.AndroidX.CardView.dll => 248
	i64 u0x3235427f8d12dae1, ; 227: lib_System.Drawing.Primitives.dll.so => 35
	i64 u0x3265aeceedab907c, ; 228: fr\Microsoft.Maui.Controls.resources => 341
	i64 u0x32aa989ff07a84ff, ; 229: lib_System.Xml.ReaderWriter.dll.so => 157
	i64 u0x333b6ed338562ac5, ; 230: ru\Microsoft.Maui.Controls.resources => 357
	i64 u0x33829542f112d59b, ; 231: System.Collections.Immutable => 9
	i64 u0x33a31443733849fe, ; 232: lib-es-Microsoft.Maui.Controls.resources.dll.so => 339
	i64 u0x33e0cb4d8ee7f257, ; 233: ar\Microsoft.Maui.Controls.resources => 333
	i64 u0x340404f01807c4be, ; 234: lib_PInvoke.Windows.ShellScalingApi.dll.so => 209
	i64 u0x341abc357fbb4ebf, ; 235: lib_System.Net.Sockets.dll.so => 76
	i64 u0x3496c1e2dcaf5ecc, ; 236: lib_System.IO.Pipes.AccessControl.dll.so => 55
	i64 u0x34dfd74fe2afcf37, ; 237: Microsoft.Maui => 199
	i64 u0x34e292762d9615df, ; 238: cs/Microsoft.Maui.Controls.resources.dll => 335
	i64 u0x3508234247f48404, ; 239: Microsoft.Maui.Controls => 196
	i64 u0x353590da528c9d22, ; 240: System.ComponentModel.Annotations => 13
	i64 u0x3549870798b4cd30, ; 241: lib_Xamarin.AndroidX.ViewPager2.dll.so => 308
	i64 u0x355282fc1c909694, ; 242: Microsoft.Extensions.Configuration => 182
	i64 u0x3552fc5d578f0fbf, ; 243: Xamarin.AndroidX.Arch.Core.Common => 241
	i64 u0x355c649948d55d97, ; 244: lib_System.Runtime.Intrinsics.dll.so => 109
	i64 u0x35826dc254a3e97c, ; 245: uk\Microsoft.Maui.Controls.resources => 362
	i64 u0x35ea9d1c6834bc8c, ; 246: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 283
	i64 u0x3628ab68db23a01a, ; 247: lib_System.Diagnostics.Tools.dll.so => 32
	i64 u0x3673b042508f5b6b, ; 248: lib_System.Runtime.Extensions.dll.so => 104
	i64 u0x36740f1a8ecdc6c4, ; 249: System.Numerics => 84
	i64 u0x36b2b50fdf589ae2, ; 250: System.Reflection.Emit.Lightweight => 92
	i64 u0x36cada77dc79928b, ; 251: System.IO.MemoryMappedFiles => 53
	i64 u0x374ef46b06791af6, ; 252: System.Reflection.Primitives.dll => 96
	i64 u0x376bf93e521a5417, ; 253: lib_Xamarin.Jetbrains.Annotations.dll.so => 323
	i64 u0x37bc29f3183003b6, ; 254: lib_System.IO.dll.so => 58
	i64 u0x380134e03b1e160a, ; 255: System.Collections.Immutable.dll => 9
	i64 u0x38049b5c59b39324, ; 256: System.Runtime.CompilerServices.Unsafe => 102
	i64 u0x3834413b35ff4d19, ; 257: hi\Microsoft.Maui.Controls.resources => 343
	i64 u0x385c17636bb6fe6e, ; 258: Xamarin.AndroidX.CustomView.dll => 259
	i64 u0x38869c811d74050e, ; 259: System.Net.NameResolution.dll => 68
	i64 u0x393c226616977fdb, ; 260: lib_Xamarin.AndroidX.ViewPager.dll.so => 307
	i64 u0x395e37c3334cf82a, ; 261: lib-ca-Microsoft.Maui.Controls.resources.dll.so => 334
	i64 u0x39aa39fda111d9d3, ; 262: Newtonsoft.Json => 204
	i64 u0x3ab5859054645f72, ; 263: System.Security.Cryptography.Primitives.dll => 125
	i64 u0x3ad75090c3fac0e9, ; 264: lib_Xamarin.AndroidX.ResourceInspection.Annotation.dll.so => 295
	i64 u0x3ae44ac43a1fbdbb, ; 265: System.Runtime.Serialization => 116
	i64 u0x3b860f9932505633, ; 266: lib_System.Text.Encoding.Extensions.dll.so => 135
	i64 u0x3bea9ebe8c027c01, ; 267: lib_Microsoft.IdentityModel.Tokens.dll.so => 194
	i64 u0x3c3aafb6b3a00bf6, ; 268: lib_System.Security.Cryptography.X509Certificates.dll.so => 126
	i64 u0x3c4049146b59aa90, ; 269: System.Runtime.InteropServices.JavaScript => 106
	i64 u0x3c7c495f58ac5ee9, ; 270: Xamarin.Kotlin.StdLib => 324
	i64 u0x3c7e5ed3d5db71bb, ; 271: System.Security => 131
	i64 u0x3cd9d281d402eb9b, ; 272: Xamarin.AndroidX.Browser.dll => 243
	i64 u0x3ced6a4f3010aa96, ; 273: ZXing.Net.MAUI.Controls => 332
	i64 u0x3d1c50cc001a991e, ; 274: Xamarin.Google.Guava.ListenableFuture.dll => 318
	i64 u0x3d2b1913edfc08d7, ; 275: lib_System.Threading.ThreadPool.dll.so => 147
	i64 u0x3d46f0b995082740, ; 276: System.Xml.Linq => 156
	i64 u0x3d8a8f400514a790, ; 277: Xamarin.AndroidX.Fragment.Ktx.dll => 268
	i64 u0x3d9c2a242b040a50, ; 278: lib_Xamarin.AndroidX.Core.dll.so => 256
	i64 u0x3dbb6b9f5ab90fa7, ; 279: lib_Xamarin.AndroidX.DynamicAnimation.dll.so => 263
	i64 u0x3e5441657549b213, ; 280: Xamarin.AndroidX.ResourceInspection.Annotation => 295
	i64 u0x3e57d4d195c53c2e, ; 281: System.Reflection.TypeExtensions => 97
	i64 u0x3e616ab4ed1f3f15, ; 282: lib_System.Data.dll.so => 24
	i64 u0x3f1d226e6e06db7e, ; 283: Xamarin.AndroidX.SlidingPaneLayout.dll => 299
	i64 u0x3f510adf788828dd, ; 284: System.Threading.Tasks.Extensions => 143
	i64 u0x402b3649b5f39c0a, ; 285: nl\Microsoft.Maui.Controls.resources => 352
	i64 u0x407740ff2e914d86, ; 286: Xamarin.AndroidX.Print.dll => 292
	i64 u0x407a10bb4bf95829, ; 287: lib_Xamarin.AndroidX.Navigation.Common.dll.so => 288
	i64 u0x40c98b6bd77346d4, ; 288: Microsoft.VisualBasic.dll => 3
	i64 u0x4129eb1951f3cdde, ; 289: Validation.dll => 214
	i64 u0x41833cf766d27d96, ; 290: mscorlib => 167
	i64 u0x41cab042be111c34, ; 291: lib_Xamarin.AndroidX.AppCompat.AppCompatResources.dll.so => 240
	i64 u0x423a9ecc4d905a88, ; 292: lib_System.Resources.ResourceManager.dll.so => 100
	i64 u0x423bf51ae7def810, ; 293: System.Xml.XPath => 161
	i64 u0x42462ff15ddba223, ; 294: System.Resources.Reader.dll => 99
	i64 u0x42a31b86e6ccc3f0, ; 295: System.Diagnostics.Contracts => 25
	i64 u0x430e95b891249788, ; 296: lib_System.Reflection.Emit.dll.so => 93
	i64 u0x43375950ec7c1b6a, ; 297: netstandard.dll => 168
	i64 u0x434c4e1d9284cdae, ; 298: Mono.Android.dll => 172
	i64 u0x43505013578652a0, ; 299: lib_Xamarin.AndroidX.Activity.Ktx.dll.so => 235
	i64 u0x437d06c381ed575a, ; 300: lib_Microsoft.VisualBasic.dll.so => 3
	i64 u0x43950f84de7cc79a, ; 301: pl/Microsoft.Maui.Controls.resources.dll => 353
	i64 u0x43e8ca5bc927ff37, ; 302: lib_Xamarin.AndroidX.Emoji2.ViewsHelper.dll.so => 265
	i64 u0x448bd33429269b19, ; 303: Microsoft.CSharp => 1
	i64 u0x4499fa3c8e494654, ; 304: lib_System.Runtime.Serialization.Primitives.dll.so => 114
	i64 u0x4515080865a951a5, ; 305: Xamarin.Kotlin.StdLib.dll => 324
	i64 u0x4545802489b736b9, ; 306: Xamarin.AndroidX.Fragment.Ktx => 268
	i64 u0x454b4d1e66bb783c, ; 307: Xamarin.AndroidX.Lifecycle.Process => 276
	i64 u0x458d2df79ac57c1d, ; 308: lib_System.IdentityModel.Tokens.Jwt.dll.so => 213
	i64 u0x45c40276a42e283e, ; 309: System.Diagnostics.TraceSource => 33
	i64 u0x45d443f2a29adc37, ; 310: System.AppContext.dll => 6
	i64 u0x4649773e5eba916c, ; 311: ko\Microsoft.Maui.Controls.resources => 349
	i64 u0x46a4213bc97fe5ae, ; 312: lib-ru-Microsoft.Maui.Controls.resources.dll.so => 357
	i64 u0x47358bd471172e1d, ; 313: lib_System.Xml.Linq.dll.so => 156
	i64 u0x480b7b8a3caec4c4, ; 314: lib_ExoPlayer.Database.dll.so => 220
	i64 u0x480c0a47dd42dd81, ; 315: lib_System.IO.MemoryMappedFiles.dll.so => 53
	i64 u0x488d293220a4fe37, ; 316: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 270
	i64 u0x49e952f19a4e2022, ; 317: System.ObjectModel => 85
	i64 u0x49f9e6948a8131e4, ; 318: lib_Xamarin.AndroidX.VersionedParcelable.dll.so => 306
	i64 u0x4a5667b2462a664b, ; 319: lib_Xamarin.AndroidX.Navigation.UI.dll.so => 291
	i64 u0x4a7a18981dbd56bc, ; 320: System.IO.Compression.FileSystem.dll => 44
	i64 u0x4aa5c60350917c06, ; 321: lib_Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll.so => 275
	i64 u0x4b07a0ed0ab33ff4, ; 322: System.Runtime.Extensions.dll => 104
	i64 u0x4b1293102404919d, ; 323: ExoPlayer.Database => 220
	i64 u0x4b576d47ac054f3c, ; 324: System.IO.FileSystem.AccessControl => 47
	i64 u0x4b7b6532ded934b7, ; 325: System.Text.Json => 138
	i64 u0x4c4ca2dbd985c7c2, ; 326: ExoPlayer.Container.dll => 217
	i64 u0x4c7755cf07ad2d5f, ; 327: System.Net.Http.Json.dll => 64
	i64 u0x4c9caee94c082049, ; 328: Xamarin.GooglePlayServices.Maps => 321
	i64 u0x4cc5f15266470798, ; 329: lib_Xamarin.AndroidX.Loader.dll.so => 285
	i64 u0x4cf6f67dc77aacd2, ; 330: System.Net.NetworkInformation.dll => 69
	i64 u0x4d3183dd245425d4, ; 331: System.Net.WebSockets.Client.dll => 80
	i64 u0x4d479f968a05e504, ; 332: System.Linq.Expressions.dll => 59
	i64 u0x4d55a010ffc4faff, ; 333: System.Private.Xml => 89
	i64 u0x4d5cbe77561c5b2e, ; 334: System.Web.dll => 154
	i64 u0x4d77512dbd86ee4c, ; 335: lib_Xamarin.AndroidX.Arch.Core.Common.dll.so => 241
	i64 u0x4d7793536e79c309, ; 336: System.ServiceProcess => 133
	i64 u0x4d95fccc1f67c7ca, ; 337: System.Runtime.Loader.dll => 110
	i64 u0x4da4a8f0f6a70fdc, ; 338: Microsoft.Maui.Controls.Compatibility.dll => 195
	i64 u0x4dcf44c3c9b076a2, ; 339: it/Microsoft.Maui.Controls.resources.dll => 347
	i64 u0x4dd9247f1d2c3235, ; 340: Xamarin.AndroidX.Loader.dll => 285
	i64 u0x4e1d4786fe965eeb, ; 341: ExoPlayer.DataSource.dll => 221
	i64 u0x4e2aeee78e2c4a87, ; 342: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 293
	i64 u0x4e32f00cb0937401, ; 343: Mono.Android.Runtime => 171
	i64 u0x4e5eea4668ac2b18, ; 344: System.Text.Encoding.CodePages => 134
	i64 u0x4ebd0c4b82c5eefc, ; 345: lib_System.Threading.Channels.dll.so => 140
	i64 u0x4ee8eaa9c9c1151a, ; 346: System.Globalization.Calendars => 40
	i64 u0x4ef2a044e31c8432, ; 347: QRCoder.dll => 211
	i64 u0x4f2082c3b43d6396, ; 348: lib_CommunityToolkit.Diagnostics.dll.so => 175
	i64 u0x4f92a38a50262aca, ; 349: el\Microsoft.Maui.Controls.resources => 338
	i64 u0x4ffd65baff757598, ; 350: Microsoft.IdentityModel.Tokens => 194
	i64 u0x5037f0be3c28c7a3, ; 351: lib_Microsoft.Maui.Controls.dll.so => 196
	i64 u0x506203448c473a65, ; 352: Xamarin.Google.AutoValue.Annotations => 312
	i64 u0x50c3a29b21050d45, ; 353: System.Linq.Parallel.dll => 60
	i64 u0x5112ed116d87baf8, ; 354: CommunityToolkit.Mvvm => 180
	i64 u0x5131bbe80989093f, ; 355: Xamarin.AndroidX.Lifecycle.ViewModel.Android.dll => 282
	i64 u0x516324a5050a7e3c, ; 356: System.Net.WebProxy => 79
	i64 u0x516d6f0b21a303de, ; 357: lib_System.Diagnostics.Contracts.dll.so => 25
	i64 u0x51bb8a2afe774e32, ; 358: System.Drawing => 36
	i64 u0x5247c5c32a4140f0, ; 359: System.Resources.Reader => 99
	i64 u0x526bb15e3c386364, ; 360: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 279
	i64 u0x526ce79eb8e90527, ; 361: lib_System.Net.Primitives.dll.so => 71
	i64 u0x52829f00b4467c38, ; 362: lib_System.Data.Common.dll.so => 22
	i64 u0x529ffe06f39ab8db, ; 363: Xamarin.AndroidX.Core => 256
	i64 u0x52ff996554dbf352, ; 364: Microsoft.Maui.Graphics => 201
	i64 u0x535f7e40e8fef8af, ; 365: lib-sk-Microsoft.Maui.Controls.resources.dll.so => 358
	i64 u0x53856f5f890e77fb, ; 366: ExoPlayer.UI.dll => 229
	i64 u0x53978aac584c666e, ; 367: lib_System.Security.Cryptography.Cng.dll.so => 121
	i64 u0x53a96d5c86c9e194, ; 368: System.Net.NetworkInformation => 69
	i64 u0x53be1038a61e8d44, ; 369: System.Runtime.InteropServices.RuntimeInformation.dll => 107
	i64 u0x53c3014b9437e684, ; 370: lib-zh-HK-Microsoft.Maui.Controls.resources.dll.so => 364
	i64 u0x53e450ebd586f842, ; 371: lib_Xamarin.AndroidX.LocalBroadcastManager.dll.so => 286
	i64 u0x5435e6f049e9bc37, ; 372: System.Security.Claims.dll => 119
	i64 u0x54795225dd1587af, ; 373: lib_System.Runtime.dll.so => 117
	i64 u0x547a34f14e5f6210, ; 374: Xamarin.AndroidX.Lifecycle.Common.dll => 271
	i64 u0x54e0759a7e57a36e, ; 375: lib_MvvmHelpers.dll.so => 212
	i64 u0x554c31f797bb0485, ; 376: lib_ExoPlayer.DataSource.dll.so => 221
	i64 u0x556e8b63b660ab8b, ; 377: Xamarin.AndroidX.Lifecycle.Common.Jvm.dll => 272
	i64 u0x5588627c9a108ec9, ; 378: System.Collections.Specialized => 11
	i64 u0x55a898e4f42e3fae, ; 379: Microsoft.VisualBasic.Core.dll => 2
	i64 u0x55fa0c610fe93bb1, ; 380: lib_System.Security.Cryptography.OpenSsl.dll.so => 124
	i64 u0x56442b99bc64bb47, ; 381: System.Runtime.Serialization.Xml.dll => 115
	i64 u0x56a8b26e1aeae27b, ; 382: System.Threading.Tasks.Dataflow => 142
	i64 u0x56f932d61e93c07f, ; 383: System.Globalization.Extensions => 41
	i64 u0x571c5cfbec5ae8e2, ; 384: System.Private.Uri => 87
	i64 u0x57201164aeb974e3, ; 385: Xamarin.Google.Guava.FailureAccess.dll => 317
	i64 u0x576499c9f52fea31, ; 386: Xamarin.AndroidX.Annotation => 236
	i64 u0x579a06fed6eec900, ; 387: System.Private.CoreLib.dll => 173
	i64 u0x57c542c14049b66d, ; 388: System.Diagnostics.DiagnosticSource => 27
	i64 u0x581a8bd5cfda563e, ; 389: System.Threading.Timer => 148
	i64 u0x58601b2dda4a27b9, ; 390: lib-ja-Microsoft.Maui.Controls.resources.dll.so => 348
	i64 u0x58688d9af496b168, ; 391: Microsoft.Extensions.DependencyInjection.dll => 184
	i64 u0x588c167a79db6bfb, ; 392: lib_Xamarin.Google.ErrorProne.Annotations.dll.so => 315
	i64 u0x5906028ae5151104, ; 393: Xamarin.AndroidX.Activity.Ktx => 235
	i64 u0x595a356d23e8da9a, ; 394: lib_Microsoft.CSharp.dll.so => 1
	i64 u0x59cd492ef1d088d6, ; 395: ExoPlayer.Rtsp => 226
	i64 u0x59f9e60b9475085f, ; 396: lib_Xamarin.AndroidX.Annotation.Experimental.dll.so => 237
	i64 u0x5a745f5101a75527, ; 397: lib_System.IO.Compression.FileSystem.dll.so => 44
	i64 u0x5a89a886ae30258d, ; 398: lib_Xamarin.AndroidX.CoordinatorLayout.dll.so => 255
	i64 u0x5a8f6699f4a1caa9, ; 399: lib_System.Threading.dll.so => 149
	i64 u0x5ab00a90c2a770a1, ; 400: ms\Microsoft.Maui.Controls.resources => 350
	i64 u0x5ae8e4f3eae4d547, ; 401: Xamarin.AndroidX.Legacy.Support.Core.Utils => 270
	i64 u0x5ae9cd33b15841bf, ; 402: System.ComponentModel => 18
	i64 u0x5aeb8cd498d4823e, ; 403: lib_Xamarin.Google.Guava.dll.so => 316
	i64 u0x5b54391bdc6fcfe6, ; 404: System.Private.DataContractSerialization => 86
	i64 u0x5b5f0e240a06a2a2, ; 405: da/Microsoft.Maui.Controls.resources.dll => 336
	i64 u0x5b755276902c8414, ; 406: Xamarin.GooglePlayServices.Base => 319
	i64 u0x5b8109e8e14c5e3e, ; 407: System.Globalization.Extensions.dll => 41
	i64 u0x5bddd04d72a9e350, ; 408: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 275
	i64 u0x5bdf16b09da116ab, ; 409: Xamarin.AndroidX.Collection => 249
	i64 u0x5be51811e04300fd, ; 410: PInvoke.Windows.Core => 208
	i64 u0x5c019d5266093159, ; 411: lib_Xamarin.AndroidX.Lifecycle.Runtime.Ktx.Android.dll.so => 280
	i64 u0x5c30a4a35f9cc8c4, ; 412: lib_System.Reflection.Extensions.dll.so => 94
	i64 u0x5c393624b8176517, ; 413: lib_Microsoft.Extensions.Logging.dll.so => 186
	i64 u0x5c53c29f5073b0c9, ; 414: System.Diagnostics.FileVersionInfo => 28
	i64 u0x5c87463c575c7616, ; 415: lib_System.Globalization.Extensions.dll.so => 41
	i64 u0x5d0a4a29b02d9d3c, ; 416: System.Net.WebHeaderCollection.dll => 78
	i64 u0x5d1b514fc45c92d4, ; 417: ZXing.Net.MAUI => 331
	i64 u0x5d40c9b15181641f, ; 418: lib_Xamarin.AndroidX.Emoji2.dll.so => 264
	i64 u0x5d6ca10d35e9485b, ; 419: lib_Xamarin.AndroidX.Concurrent.Futures.dll.so => 252
	i64 u0x5d7ec76c1c703055, ; 420: System.Threading.Tasks.Parallel => 144
	i64 u0x5db0cbbd1028510e, ; 421: lib_System.Runtime.InteropServices.dll.so => 108
	i64 u0x5db30905d3e5013b, ; 422: Xamarin.AndroidX.Collection.Jvm.dll => 250
	i64 u0x5e0157b67ec3b60e, ; 423: Microsoft.Toolkit.Uwp.Notifications.dll => 203
	i64 u0x5e467bc8f09ad026, ; 424: System.Collections.Specialized.dll => 11
	i64 u0x5e5173b3208d97e7, ; 425: System.Runtime.Handles.dll => 105
	i64 u0x5ea92fdb19ec8c4c, ; 426: System.Text.Encodings.Web.dll => 137
	i64 u0x5eb8046dd40e9ac3, ; 427: System.ComponentModel.Primitives => 16
	i64 u0x5ec272d219c9aba4, ; 428: System.Security.Cryptography.Csp.dll => 122
	i64 u0x5eee1376d94c7f5e, ; 429: System.Net.HttpListener.dll => 66
	i64 u0x5f36ccf5c6a57e24, ; 430: System.Xml.ReaderWriter.dll => 157
	i64 u0x5f4294b9b63cb842, ; 431: System.Data.Common => 22
	i64 u0x5f9a2d823f664957, ; 432: lib-el-Microsoft.Maui.Controls.resources.dll.so => 338
	i64 u0x5fac98e0b37a5b9d, ; 433: System.Runtime.CompilerServices.Unsafe.dll => 102
	i64 u0x609f4b7b63d802d4, ; 434: lib_Microsoft.Extensions.DependencyInjection.dll.so => 184
	i64 u0x60cd4e33d7e60134, ; 435: Xamarin.KotlinX.Coroutines.Core.Jvm => 329
	i64 u0x60f62d786afcf130, ; 436: System.Memory => 63
	i64 u0x61bb78c89f867353, ; 437: System.IO => 58
	i64 u0x61be8d1299194243, ; 438: Microsoft.Maui.Controls.Xaml => 198
	i64 u0x61d88f399afb2f45, ; 439: lib_System.Runtime.Loader.dll.so => 110
	i64 u0x622eef6f9e59068d, ; 440: System.Private.CoreLib => 173
	i64 u0x63982c87366f9be8, ; 441: Xamarin.Google.Guava => 316
	i64 u0x63d5e3aa4ef9b931, ; 442: Xamarin.KotlinX.Coroutines.Android.dll => 327
	i64 u0x63f1f6883c1e23c2, ; 443: lib_System.Collections.Immutable.dll.so => 9
	i64 u0x6400f68068c1e9f1, ; 444: Xamarin.Google.Android.Material.dll => 311
	i64 u0x640e3b14dbd325c2, ; 445: System.Security.Cryptography.Algorithms.dll => 120
	i64 u0x64587004560099b9, ; 446: System.Reflection => 98
	i64 u0x64b1529a438a3c45, ; 447: lib_System.Runtime.Handles.dll.so => 105
	i64 u0x6565fba2cd8f235b, ; 448: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 283
	i64 u0x658f524e4aba7dad, ; 449: CommunityToolkit.Maui.dll => 176
	i64 u0x65ecac39144dd3cc, ; 450: Microsoft.Maui.Controls.dll => 196
	i64 u0x65ece51227bfa724, ; 451: lib_System.Runtime.Numerics.dll.so => 111
	i64 u0x660ef3f628e57380, ; 452: pt-BR\Microsoft.Maui.Controls.resources => 354
	i64 u0x661722438787b57f, ; 453: Xamarin.AndroidX.Annotation.Jvm.dll => 238
	i64 u0x662219ad99512fc0, ; 454: zh-Hans\Microsoft.Maui.Controls.resources => 365
	i64 u0x6679b2337ee6b22a, ; 455: lib_System.IO.FileSystem.Primitives.dll.so => 49
	i64 u0x6692e924eade1b29, ; 456: lib_System.Console.dll.so => 20
	i64 u0x66a4e5c6a3fb0bae, ; 457: lib_Xamarin.AndroidX.Lifecycle.ViewModel.Android.dll.so => 282
	i64 u0x66d13304ce1a3efa, ; 458: Xamarin.AndroidX.CursorAdapter => 258
	i64 u0x674303f65d8fad6f, ; 459: lib_System.Net.Quic.dll.so => 72
	i64 u0x6756ca4cad62e9d6, ; 460: lib_Xamarin.AndroidX.ConstraintLayout.Core.dll.so => 254
	i64 u0x67c0802770244408, ; 461: System.Windows.dll => 155
	i64 u0x68100b69286e27cd, ; 462: lib_System.Formats.Tar.dll.so => 39
	i64 u0x68558ec653afa616, ; 463: lib-da-Microsoft.Maui.Controls.resources.dll.so => 336
	i64 u0x6872ec7a2e36b1ac, ; 464: System.Drawing.Primitives.dll => 35
	i64 u0x68bb2c417aa9b61c, ; 465: Xamarin.KotlinX.AtomicFU.dll => 325
	i64 u0x68fbbbe2eb455198, ; 466: System.Formats.Asn1 => 38
	i64 u0x69063fc0ba8e6bdd, ; 467: he/Microsoft.Maui.Controls.resources.dll => 342
	i64 u0x692b2e53fec369cf, ; 468: lib_CommunityToolkit.Maui.Maps.dll.so => 178
	i64 u0x69a3e26c76f6eec4, ; 469: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 310
	i64 u0x6a4d5ab9971a9225, ; 470: CommunityToolkit.Maui.Maps.dll => 178
	i64 u0x6a4d7577b2317255, ; 471: System.Runtime.InteropServices.dll => 108
	i64 u0x6afcedb171067e2b, ; 472: System.Core.dll => 21
	i64 u0x6bef98e124147c24, ; 473: Xamarin.Jetbrains.Annotations => 323
	i64 u0x6ce874bff138ce2b, ; 474: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 281
	i64 u0x6d12bfaa99c72b1f, ; 475: lib_Microsoft.Maui.Graphics.dll.so => 201
	i64 u0x6d70755158ca866e, ; 476: lib_System.ComponentModel.EventBasedAsync.dll.so => 15
	i64 u0x6d79993361e10ef2, ; 477: Microsoft.Extensions.Primitives => 190
	i64 u0x6d7eeca99577fc8b, ; 478: lib_System.Net.WebProxy.dll.so => 79
	i64 u0x6d8515b19946b6a2, ; 479: System.Net.WebProxy.dll => 79
	i64 u0x6d86d56b84c8eb71, ; 480: lib_Xamarin.AndroidX.CursorAdapter.dll.so => 258
	i64 u0x6d9bea6b3e895cf7, ; 481: Microsoft.Extensions.Primitives.dll => 190
	i64 u0x6dd9bf4083de3f6a, ; 482: Xamarin.AndroidX.DocumentFile.dll => 261
	i64 u0x6e25a02c3833319a, ; 483: lib_Xamarin.AndroidX.Navigation.Fragment.dll.so => 289
	i64 u0x6e79c6bd8627412a, ; 484: Xamarin.AndroidX.SavedState.SavedState.Ktx => 297
	i64 u0x6e838d9a2a6f6c9e, ; 485: lib_System.ValueTuple.dll.so => 152
	i64 u0x6e9965ce1095e60a, ; 486: lib_System.Core.dll.so => 21
	i64 u0x6fd2265da78b93a4, ; 487: lib_Microsoft.Maui.dll.so => 199
	i64 u0x6ffc4967cc47ba57, ; 488: System.IO.FileSystem.Watcher.dll => 50
	i64 u0x701cd46a1c25a5fe, ; 489: System.IO.FileSystem.dll => 51
	i64 u0x709688f4ec2831e9, ; 490: lib_Xamarin.AndroidX.Media.dll.so => 287
	i64 u0x70e99f48c05cb921, ; 491: tr/Microsoft.Maui.Controls.resources.dll => 361
	i64 u0x70fd3deda22442d2, ; 492: lib-nb-Microsoft.Maui.Controls.resources.dll.so => 351
	i64 u0x71485e7ffdb4b958, ; 493: System.Reflection.Extensions => 94
	i64 u0x7162a2fce67a945f, ; 494: lib_Xamarin.Android.Glide.Annotations.dll.so => 231
	i64 u0x71a495ea3761dde8, ; 495: lib-it-Microsoft.Maui.Controls.resources.dll.so => 347
	i64 u0x71ad672adbe48f35, ; 496: System.ComponentModel.Primitives.dll => 16
	i64 u0x725f5a9e82a45c81, ; 497: System.Security.Cryptography.Encoding => 123
	i64 u0x72b1fb4109e08d7b, ; 498: lib-hr-Microsoft.Maui.Controls.resources.dll.so => 344
	i64 u0x72e0300099accce1, ; 499: System.Xml.XPath.XDocument => 160
	i64 u0x730bfb248998f67a, ; 500: System.IO.Compression.ZipFile => 45
	i64 u0x732b2d67b9e5c47b, ; 501: Xamarin.Google.ErrorProne.Annotations.dll => 315
	i64 u0x734b76fdc0dc05bb, ; 502: lib_GoogleGson.dll.so => 181
	i64 u0x73a6be34e822f9d1, ; 503: lib_System.Runtime.Serialization.dll.so => 116
	i64 u0x73e4ce94e2eb6ffc, ; 504: lib_System.Memory.dll.so => 63
	i64 u0x743a1eccf080489a, ; 505: WindowsBase.dll => 166
	i64 u0x755a91767330b3d4, ; 506: lib_Microsoft.Extensions.Configuration.dll.so => 182
	i64 u0x75c326eb821b85c4, ; 507: lib_System.ComponentModel.DataAnnotations.dll.so => 14
	i64 u0x76012e7334db86e5, ; 508: lib_Xamarin.AndroidX.SavedState.dll.so => 296
	i64 u0x76933451f781da35, ; 509: lib_PInvoke.User32.dll.so => 207
	i64 u0x76ca07b878f44da0, ; 510: System.Runtime.Numerics.dll => 111
	i64 u0x7736c8a96e51a061, ; 511: lib_Xamarin.AndroidX.Annotation.Jvm.dll.so => 238
	i64 u0x778a805e625329ef, ; 512: System.Linq.Parallel => 60
	i64 u0x779290cc2b801eb7, ; 513: Xamarin.KotlinX.AtomicFU.Jvm => 326
	i64 u0x77bf40592cd67602, ; 514: Xamarin.Google.AutoValue.Annotations.dll => 312
	i64 u0x77f8a4acc2fdc449, ; 515: System.Security.Cryptography.Cng.dll => 121
	i64 u0x780bc73597a503a9, ; 516: lib-ms-Microsoft.Maui.Controls.resources.dll.so => 350
	i64 u0x782c5d8eb99ff201, ; 517: lib_Microsoft.VisualBasic.Core.dll.so => 2
	i64 u0x783606d1e53e7a1a, ; 518: th/Microsoft.Maui.Controls.resources.dll => 360
	i64 u0x78a45e51311409b6, ; 519: Xamarin.AndroidX.Fragment.dll => 267
	i64 u0x78ed4ab8f9d800a1, ; 520: Xamarin.AndroidX.Lifecycle.ViewModel => 281
	i64 u0x79e2fd2773a12b40, ; 521: lib_CommunityToolkit.Maui.MediaElement.dll.so => 179
	i64 u0x7a39601d6f0bb831, ; 522: lib_Xamarin.KotlinX.AtomicFU.dll.so => 325
	i64 u0x7a7e7eddf79c5d26, ; 523: lib_Xamarin.AndroidX.Lifecycle.ViewModel.dll.so => 281
	i64 u0x7a9a57d43b0845fa, ; 524: System.AppContext => 6
	i64 u0x7ad0f4f1e5d08183, ; 525: Xamarin.AndroidX.Collection.dll => 249
	i64 u0x7adb8da2ac89b647, ; 526: fi/Microsoft.Maui.Controls.resources.dll => 340
	i64 u0x7b13d9eaa944ade8, ; 527: Xamarin.AndroidX.DynamicAnimation.dll => 263
	i64 u0x7b3618c2f8072f70, ; 528: da\Microsoft.Maui.Controls.resources => 336
	i64 u0x7b4927e421291c41, ; 529: Microsoft.IdentityModel.JsonWebTokens.dll => 192
	i64 u0x7bef86a4335c4870, ; 530: System.ComponentModel.TypeConverter => 17
	i64 u0x7c0820144cd34d6a, ; 531: sk/Microsoft.Maui.Controls.resources.dll => 358
	i64 u0x7c2a0bd1e0f988fc, ; 532: lib-de-Microsoft.Maui.Controls.resources.dll.so => 337
	i64 u0x7c41d387501568ba, ; 533: System.Net.WebClient.dll => 77
	i64 u0x7c482cd79bd24b13, ; 534: lib_Xamarin.AndroidX.ConstraintLayout.dll.so => 253
	i64 u0x7c802877aa7f24e0, ; 535: cs\Microsoft.Maui.Controls.resources => 335
	i64 u0x7c8cb8cf04bee12b, ; 536: lib_Xamarin.Google.AutoValue.Annotations.dll.so => 312
	i64 u0x7cb95ad2a929d044, ; 537: Xamarin.GooglePlayServices.Basement => 320
	i64 u0x7cc637f941f716d0, ; 538: CommunityToolkit.Maui.Core => 177
	i64 u0x7cd2ec8eaf5241cd, ; 539: System.Security.dll => 131
	i64 u0x7cf9ae50dd350622, ; 540: Xamarin.Jetbrains.Annotations.dll => 323
	i64 u0x7d649b75d580bb42, ; 541: ms/Microsoft.Maui.Controls.resources.dll => 350
	i64 u0x7d8ee2bdc8e3aad1, ; 542: System.Numerics.Vectors => 83
	i64 u0x7df5df8db8eaa6ac, ; 543: Microsoft.Extensions.Logging.Debug => 188
	i64 u0x7dfc3d6d9d8d7b70, ; 544: System.Collections => 12
	i64 u0x7e2e564fa2f76c65, ; 545: lib_System.Diagnostics.Tracing.dll.so => 34
	i64 u0x7e302e110e1e1346, ; 546: lib_System.Security.Claims.dll.so => 119
	i64 u0x7e571cad5915e6c3, ; 547: lib_Xamarin.AndroidX.Lifecycle.Process.dll.so => 276
	i64 u0x7e6b1ca712437d7d, ; 548: Xamarin.AndroidX.Emoji2.ViewsHelper => 265
	i64 u0x7e946809d6008ef2, ; 549: lib_System.ObjectModel.dll.so => 85
	i64 u0x7ea0272c1b4a9635, ; 550: lib_Xamarin.Android.Glide.dll.so => 230
	i64 u0x7eb4f0dc47488736, ; 551: lib_Xamarin.GooglePlayServices.Tasks.dll.so => 322
	i64 u0x7ecc13347c8fd849, ; 552: lib_System.ComponentModel.dll.so => 18
	i64 u0x7f00ddd9b9ca5a13, ; 553: Xamarin.AndroidX.ViewPager.dll => 307
	i64 u0x7f9351cd44b1273f, ; 554: Microsoft.Extensions.Configuration.Abstractions => 183
	i64 u0x7fbd557c99b3ce6f, ; 555: lib_Xamarin.AndroidX.Lifecycle.LiveData.Core.dll.so => 274
	i64 u0x7fde679093d95532, ; 556: lib_ExoPlayer.Container.dll.so => 217
	i64 u0x8076a9a44a2ca331, ; 557: System.Net.Quic => 72
	i64 u0x80b7e726b0280681, ; 558: Microsoft.VisualStudio.DesignTools.MobileTapContracts => 368
	i64 u0x80da183a87731838, ; 559: System.Reflection.Metadata => 95
	i64 u0x812c069d5cdecc17, ; 560: System.dll => 165
	i64 u0x81381be520a60adb, ; 561: Xamarin.AndroidX.Interpolator.dll => 269
	i64 u0x81657cec2b31e8aa, ; 562: System.Net => 82
	i64 u0x8277f2be6b5ce05f, ; 563: Xamarin.AndroidX.AppCompat => 239
	i64 u0x828f06563b30bc50, ; 564: lib_Xamarin.AndroidX.CardView.dll.so => 248
	i64 u0x82920a8d9194a019, ; 565: Xamarin.KotlinX.AtomicFU.Jvm.dll => 326
	i64 u0x82b399cb01b531c4, ; 566: lib_System.Web.dll.so => 154
	i64 u0x82df8f5532a10c59, ; 567: lib_System.Drawing.dll.so => 36
	i64 u0x82f0b6e911d13535, ; 568: lib_System.Transactions.dll.so => 151
	i64 u0x83a7afd2c49adc86, ; 569: lib_Microsoft.IdentityModel.Abstractions.dll.so => 191
	i64 u0x844ac8f64fd78edc, ; 570: Xamarin.AndroidX.Camera.View.dll => 247
	i64 u0x846ce984efea52c7, ; 571: System.Threading.Tasks.Parallel.dll => 144
	i64 u0x847d90a9583b3cae, ; 572: lib_Youth.dll.so => 0
	i64 u0x84ae73148a4557d2, ; 573: lib_System.IO.Pipes.dll.so => 56
	i64 u0x84b01102c12a9232, ; 574: System.Runtime.Serialization.Json.dll => 113
	i64 u0x850c5ba0b57ce8e7, ; 575: lib_Xamarin.AndroidX.Collection.dll.so => 249
	i64 u0x851d02edd334b044, ; 576: Xamarin.AndroidX.VectorDrawable => 304
	i64 u0x85c919db62150978, ; 577: Xamarin.AndroidX.Transition.dll => 303
	i64 u0x8662aaeb94fef37f, ; 578: lib_System.Dynamic.Runtime.dll.so => 37
	i64 u0x86a909228dc7657b, ; 579: lib-zh-Hant-Microsoft.Maui.Controls.resources.dll.so => 366
	i64 u0x86b3e00c36b84509, ; 580: Microsoft.Extensions.Configuration.dll => 182
	i64 u0x86b62cb077ec4fd7, ; 581: System.Runtime.Serialization.Xml => 115
	i64 u0x86cbc49b900aeb76, ; 582: ExoPlayer => 215
	i64 u0x86e276ae35864b6a, ; 583: lib_CommunityToolkit.Common.dll.so => 174
	i64 u0x8706ffb12bf3f53d, ; 584: Xamarin.AndroidX.Annotation.Experimental => 237
	i64 u0x872a5b14c18d328c, ; 585: System.ComponentModel.DataAnnotations => 14
	i64 u0x872fb9615bc2dff0, ; 586: Xamarin.Android.Glide.Annotations.dll => 231
	i64 u0x87c69b87d9283884, ; 587: lib_System.Threading.Thread.dll.so => 146
	i64 u0x87f6569b25707834, ; 588: System.IO.Compression.Brotli.dll => 43
	i64 u0x8842b3a5d2d3fb36, ; 589: Microsoft.Maui.Essentials => 200
	i64 u0x88926583efe7ee86, ; 590: Xamarin.AndroidX.Activity.Ktx.dll => 235
	i64 u0x88ba6bc4f7762b03, ; 591: lib_System.Reflection.dll.so => 98
	i64 u0x88bda98e0cffb7a9, ; 592: lib_Xamarin.KotlinX.Coroutines.Core.Jvm.dll.so => 329
	i64 u0x8930322c7bd8f768, ; 593: netstandard => 168
	i64 u0x897a606c9e39c75f, ; 594: lib_System.ComponentModel.Primitives.dll.so => 16
	i64 u0x89911a22005b92b7, ; 595: System.IO.FileSystem.DriveInfo.dll => 48
	i64 u0x89c5188089ec2cd5, ; 596: lib_System.Runtime.InteropServices.RuntimeInformation.dll.so => 107
	i64 u0x8a19e3dc71b34b2c, ; 597: System.Reflection.TypeExtensions.dll => 97
	i64 u0x8ad229ea26432ee2, ; 598: Xamarin.AndroidX.Loader => 285
	i64 u0x8b4ff5d0fdd5faa1, ; 599: lib_System.Diagnostics.DiagnosticSource.dll.so => 27
	i64 u0x8b541d476eb3774c, ; 600: System.Security.Principal.Windows => 128
	i64 u0x8b8d01333a96d0b5, ; 601: System.Diagnostics.Process.dll => 29
	i64 u0x8b9ceca7acae3451, ; 602: lib-he-Microsoft.Maui.Controls.resources.dll.so => 342
	i64 u0x8cb8f612b633affb, ; 603: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 297
	i64 u0x8cdfdb4ce85fb925, ; 604: lib_System.Security.Principal.Windows.dll.so => 128
	i64 u0x8cdfe7b8f4caa426, ; 605: System.IO.Compression.FileSystem => 44
	i64 u0x8d0f420977c2c1c7, ; 606: Xamarin.AndroidX.CursorAdapter.dll => 258
	i64 u0x8d52f7ea2796c531, ; 607: Xamarin.AndroidX.Emoji2.dll => 264
	i64 u0x8d7b8ab4b3310ead, ; 608: System.Threading => 149
	i64 u0x8da188285aadfe8e, ; 609: System.Collections.Concurrent => 8
	i64 u0x8e480809c9114ba9, ; 610: he\Microsoft.Maui.Controls.resources => 342
	i64 u0x8ec6e06a61c1baeb, ; 611: lib_Newtonsoft.Json.dll.so => 204
	i64 u0x8ed807bfe9858dfc, ; 612: Xamarin.AndroidX.Navigation.Common => 288
	i64 u0x8ee08b8194a30f48, ; 613: lib-hi-Microsoft.Maui.Controls.resources.dll.so => 343
	i64 u0x8ee7a45dc27cf5a0, ; 614: th\Microsoft.Maui.Controls.resources => 360
	i64 u0x8ef7601039857a44, ; 615: lib-ro-Microsoft.Maui.Controls.resources.dll.so => 356
	i64 u0x8efbc0801a122264, ; 616: Xamarin.GooglePlayServices.Tasks.dll => 322
	i64 u0x8f32c6f611f6ffab, ; 617: pt/Microsoft.Maui.Controls.resources.dll => 355
	i64 u0x8f44b45eb046bbd1, ; 618: System.ServiceModel.Web.dll => 132
	i64 u0x8f8829d21c8985a4, ; 619: lib-pt-BR-Microsoft.Maui.Controls.resources.dll.so => 354
	i64 u0x8fbf5b0114c6dcef, ; 620: System.Globalization.dll => 42
	i64 u0x90263f8448b8f572, ; 621: lib_System.Diagnostics.TraceSource.dll.so => 33
	i64 u0x903101b46fb73a04, ; 622: _Microsoft.Android.Resource.Designer => 370
	i64 u0x90393bd4865292f3, ; 623: lib_System.IO.Compression.dll.so => 46
	i64 u0x905e2b8e7ae91ae6, ; 624: System.Threading.Tasks.Extensions.dll => 143
	i64 u0x90634f86c5ebe2b5, ; 625: Xamarin.AndroidX.Lifecycle.ViewModel.Android => 282
	i64 u0x907aee0b062c69bb, ; 626: ExoPlayer.UI => 229
	i64 u0x907b636704ad79ef, ; 627: lib_Microsoft.Maui.Controls.Xaml.dll.so => 198
	i64 u0x90e9efbfd68593e0, ; 628: lib_Xamarin.AndroidX.Lifecycle.LiveData.dll.so => 273
	i64 u0x91418dc638b29e68, ; 629: lib_Xamarin.AndroidX.CustomView.dll.so => 259
	i64 u0x9157bd523cd7ed36, ; 630: lib_System.Text.Json.dll.so => 138
	i64 u0x91a74f07b30d37e2, ; 631: System.Linq.dll => 62
	i64 u0x91af5c517fb3cf89, ; 632: CommunityToolkit.Maui.Maps => 178
	i64 u0x91cb86ea3b17111d, ; 633: System.ServiceModel.Web => 132
	i64 u0x91fa41a87223399f, ; 634: ca/Microsoft.Maui.Controls.resources.dll => 334
	i64 u0x92054e486c0c7ea7, ; 635: System.IO.FileSystem.DriveInfo => 48
	i64 u0x928614058c40c4cd, ; 636: lib_System.Xml.XPath.XDocument.dll.so => 160
	i64 u0x92b138fffca2b01e, ; 637: lib_Xamarin.AndroidX.Arch.Core.Runtime.dll.so => 242
	i64 u0x92dfc2bfc6c6a888, ; 638: Xamarin.AndroidX.Lifecycle.LiveData => 273
	i64 u0x933da2c779423d68, ; 639: Xamarin.Android.Glide.Annotations => 231
	i64 u0x9388aad9b7ae40ce, ; 640: lib_Xamarin.AndroidX.Lifecycle.Common.dll.so => 271
	i64 u0x93fe668a5ec0ac46, ; 641: Microsoft.Toolkit.Uwp.Notifications => 203
	i64 u0x941c00d21e5c0679, ; 642: lib_Xamarin.AndroidX.Transition.dll.so => 303
	i64 u0x944077d8ca3c6580, ; 643: System.IO.Compression.dll => 46
	i64 u0x948cffedc8ed7960, ; 644: System.Xml => 164
	i64 u0x948d746a7702861f, ; 645: Microsoft.IdentityModel.Logging.dll => 193
	i64 u0x94c8990839c4bdb1, ; 646: lib_Xamarin.AndroidX.Interpolator.dll.so => 269
	i64 u0x94e71ba9e04481f1, ; 647: ja\Microsoft.Maui.Controls.resources => 348
	i64 u0x94ec641640d81fc4, ; 648: ExoPlayer.Decoder => 222
	i64 u0x9564283c37ed59a9, ; 649: lib_Microsoft.IdentityModel.Logging.dll.so => 193
	i64 u0x95c6b36f5f5d7039, ; 650: Xamarin.AndroidX.Camera.Camera2 => 244
	i64 u0x95d757769563d0d3, ; 651: Xamarin.AndroidX.Camera.Lifecycle.dll => 246
	i64 u0x9686161486d34b81, ; 652: lib_Xamarin.AndroidX.ExifInterface.dll.so => 266
	i64 u0x978be80e5210d31b, ; 653: Microsoft.Maui.Graphics.dll => 201
	i64 u0x979ab54025cc1c7f, ; 654: lib_Xamarin.GooglePlayServices.Base.dll.so => 319
	i64 u0x97b8c771ea3e4220, ; 655: System.ComponentModel.dll => 18
	i64 u0x97e144c9d3c6976e, ; 656: System.Collections.Concurrent.dll => 8
	i64 u0x984184e3c70d4419, ; 657: GoogleGson => 181
	i64 u0x9843944103683dd3, ; 658: Xamarin.AndroidX.Core.Core.Ktx => 257
	i64 u0x98d720cc4597562c, ; 659: System.Security.Cryptography.OpenSsl => 124
	i64 u0x99052c1297204af4, ; 660: lib_Xamarin.AndroidX.Camera.Core.dll.so => 245
	i64 u0x991d510397f92d9d, ; 661: System.Linq.Expressions => 59
	i64 u0x993cc632e821c001, ; 662: Microsoft.Maui.Controls.Compatibility => 195
	i64 u0x996ceeb8a3da3d67, ; 663: System.Threading.Overlapped.dll => 141
	i64 u0x999cb19e1a04ffd3, ; 664: CommunityToolkit.Mvvm.dll => 180
	i64 u0x99a00ca5270c6878, ; 665: Xamarin.AndroidX.Navigation.Runtime => 290
	i64 u0x99cdc6d1f2d3a72f, ; 666: ko/Microsoft.Maui.Controls.resources.dll => 349
	i64 u0x9a01b1da98b6ee10, ; 667: Xamarin.AndroidX.Lifecycle.Runtime.dll => 277
	i64 u0x9a5ccc274fd6e6ee, ; 668: Jsr305Binding.dll => 313
	i64 u0x9ae6940b11c02876, ; 669: lib_Xamarin.AndroidX.Window.dll.so => 309
	i64 u0x9b211a749105beac, ; 670: System.Transactions.Local => 150
	i64 u0x9b8734714671022d, ; 671: System.Threading.Tasks.Dataflow.dll => 142
	i64 u0x9bc6aea27fbf034f, ; 672: lib_Xamarin.KotlinX.Coroutines.Core.dll.so => 328
	i64 u0x9bd8cc74558ad4c7, ; 673: Xamarin.KotlinX.AtomicFU => 325
	i64 u0x9c244ac7cda32d26, ; 674: System.Security.Cryptography.X509Certificates.dll => 126
	i64 u0x9c465f280cf43733, ; 675: lib_Xamarin.KotlinX.Coroutines.Android.dll.so => 327
	i64 u0x9c7091f94d5e494f, ; 676: ExoPlayer.Container => 217
	i64 u0x9c8f6872beab6408, ; 677: System.Xml.XPath.XDocument.dll => 160
	i64 u0x9c9ac1d95198ac57, ; 678: Newtonsoft.Json.Bson.dll => 205
	i64 u0x9ce01cf91101ae23, ; 679: System.Xml.XmlDocument => 162
	i64 u0x9d128180c81d7ce6, ; 680: Xamarin.AndroidX.CustomView.PoolingContainer => 260
	i64 u0x9d5dbcf5a48583fe, ; 681: lib_Xamarin.AndroidX.Activity.dll.so => 234
	i64 u0x9d74dee1a7725f34, ; 682: Microsoft.Extensions.Configuration.Abstractions.dll => 183
	i64 u0x9e407941c0f473ed, ; 683: lib_PInvoke.Windows.Core.dll.so => 208
	i64 u0x9e4b95dec42769f7, ; 684: System.Diagnostics.Debug.dll => 26
	i64 u0x9eaf1efdf6f7267e, ; 685: Xamarin.AndroidX.Navigation.Common.dll => 288
	i64 u0x9eeec7beda8fafa4, ; 686: BouncyCastle.Crypto.dll => 210
	i64 u0x9ef542cf1f78c506, ; 687: Xamarin.AndroidX.Lifecycle.LiveData.Core => 274
	i64 u0x9ff334e3cf272fd6, ; 688: lib_Xamarin.AndroidX.Camera.Lifecycle.dll.so => 246
	i64 u0xa00832eb975f56a8, ; 689: lib_System.Net.dll.so => 82
	i64 u0xa0ad78236b7b267f, ; 690: Xamarin.AndroidX.Window => 309
	i64 u0xa0d8259f4cc284ec, ; 691: lib_System.Security.Cryptography.dll.so => 127
	i64 u0xa0e17ca50c77a225, ; 692: lib_Xamarin.Google.Crypto.Tink.Android.dll.so => 314
	i64 u0xa0ff9b3e34d92f11, ; 693: lib_System.Resources.Writer.dll.so => 101
	i64 u0xa12fbfb4da97d9f3, ; 694: System.Threading.Timer.dll => 148
	i64 u0xa1440773ee9d341e, ; 695: Xamarin.Google.Android.Material => 311
	i64 u0xa1b9d7c27f47219f, ; 696: Xamarin.AndroidX.Navigation.UI.dll => 291
	i64 u0xa2572680829d2c7c, ; 697: System.IO.Pipelines.dll => 54
	i64 u0xa26597e57ee9c7f6, ; 698: System.Xml.XmlDocument.dll => 162
	i64 u0xa308401900e5bed3, ; 699: lib_mscorlib.dll.so => 167
	i64 u0xa395572e7da6c99d, ; 700: lib_System.Security.dll.so => 131
	i64 u0xa3e683f24b43af6f, ; 701: System.Dynamic.Runtime.dll => 37
	i64 u0xa4145becdee3dc4f, ; 702: Xamarin.AndroidX.VectorDrawable.Animated => 305
	i64 u0xa4d20d2ff0563d26, ; 703: lib_CommunityToolkit.Mvvm.dll.so => 180
	i64 u0xa4eb1ba5a365fdbe, ; 704: lib_Microsoft.Toolkit.Uwp.Notifications.dll.so => 203
	i64 u0xa4edc8f2ceae241a, ; 705: System.Data.Common.dll => 22
	i64 u0xa51253457f78aa8f, ; 706: sk\Microsoft.Maui.Controls.resources => 358
	i64 u0xa5494f40f128ce6a, ; 707: System.Runtime.Serialization.Formatters.dll => 112
	i64 u0xa54b74df83dce92b, ; 708: System.Reflection.DispatchProxy => 90
	i64 u0xa579ed010d7e5215, ; 709: Xamarin.AndroidX.DocumentFile => 261
	i64 u0xa5b7152421ed6d98, ; 710: lib_System.IO.FileSystem.Watcher.dll.so => 50
	i64 u0xa5c3844f17b822db, ; 711: lib_System.Linq.Parallel.dll.so => 60
	i64 u0xa5ce5c755bde8cb8, ; 712: lib_System.Security.Cryptography.Csp.dll.so => 122
	i64 u0xa5e599d1e0524750, ; 713: System.Numerics.Vectors.dll => 83
	i64 u0xa5f1ba49b85dd355, ; 714: System.Security.Cryptography.dll => 127
	i64 u0xa61975a5a37873ea, ; 715: lib_System.Xml.XmlSerializer.dll.so => 163
	i64 u0xa6593e21584384d2, ; 716: lib_Jsr305Binding.dll.so => 313
	i64 u0xa66cbee0130865f7, ; 717: lib_WindowsBase.dll.so => 166
	i64 u0xa67dbee13e1df9ca, ; 718: Xamarin.AndroidX.SavedState.dll => 296
	i64 u0xa68493a85ce7fb99, ; 719: lib_ExoPlayer.dll.so => 215
	i64 u0xa684b098dd27b296, ; 720: lib_Xamarin.AndroidX.Security.SecurityCrypto.dll.so => 298
	i64 u0xa68a420042bb9b1f, ; 721: Xamarin.AndroidX.DrawerLayout.dll => 262
	i64 u0xa6a5b9ca6ce1c0dd, ; 722: pt\Microsoft.Maui.Controls.resources => 355
	i64 u0xa6d26156d1cacc7c, ; 723: Xamarin.Android.Glide.dll => 230
	i64 u0xa75386b5cb9595aa, ; 724: Xamarin.AndroidX.Lifecycle.Runtime.Android => 278
	i64 u0xa763fbb98df8d9fb, ; 725: lib_Microsoft.Win32.Primitives.dll.so => 4
	i64 u0xa78ce3745383236a, ; 726: Xamarin.AndroidX.Lifecycle.Common.Jvm => 272
	i64 u0xa7e1d4ed4903b807, ; 727: ExoPlayer.Ext.MediaSession.dll => 225
	i64 u0xa7eab29ed44b4e7a, ; 728: Mono.Android.Export => 170
	i64 u0xa8195217cbf017b7, ; 729: Microsoft.VisualBasic.Core => 2
	i64 u0xa843f6095f0d247d, ; 730: Xamarin.GooglePlayServices.Base.dll => 319
	i64 u0xa859a95830f367ff, ; 731: lib_Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll.so => 283
	i64 u0xa8b52f21e0dbe690, ; 732: System.Runtime.Serialization.dll => 116
	i64 u0xa8e6320dd07580ef, ; 733: lib_Microsoft.IdentityModel.JsonWebTokens.dll.so => 192
	i64 u0xa8ee4ed7de2efaee, ; 734: Xamarin.AndroidX.Annotation.dll => 236
	i64 u0xa9239858abe0b621, ; 735: PInvoke.Windows.Core.dll => 208
	i64 u0xa95590e7c57438a4, ; 736: System.Configuration => 19
	i64 u0xa964304b5631e28a, ; 737: CommunityToolkit.Maui.Core.dll => 177
	i64 u0xaa2219c8e3449ff5, ; 738: Microsoft.Extensions.Logging.Abstractions => 187
	i64 u0xaa443ac34067eeef, ; 739: System.Private.Xml.dll => 89
	i64 u0xaa52de307ef5d1dd, ; 740: System.Net.Http => 65
	i64 u0xaa9a7b0214a5cc5c, ; 741: System.Diagnostics.StackTrace.dll => 30
	i64 u0xaaaf86367285a918, ; 742: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 185
	i64 u0xab9af77b5b67a0b8, ; 743: Xamarin.AndroidX.ConstraintLayout.Core => 254
	i64 u0xab9c1b2687d86b0b, ; 744: lib_System.Linq.Expressions.dll.so => 59
	i64 u0xabc67a62badb25ab, ; 745: ExoPlayer.Decoder.dll => 222
	i64 u0xac2af3fa195a15ce, ; 746: System.Runtime.Numerics => 111
	i64 u0xac5376a2a538dc10, ; 747: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 274
	i64 u0xac5acae88f60357e, ; 748: System.Diagnostics.Tools.dll => 32
	i64 u0xac79c7e46047ad98, ; 749: System.Security.Principal.Windows.dll => 128
	i64 u0xac98d31068e24591, ; 750: System.Xml.XDocument => 159
	i64 u0xacdd9e4180d56dda, ; 751: Xamarin.AndroidX.Concurrent.Futures => 252
	i64 u0xacf42eea7ef9cd12, ; 752: System.Threading.Channels => 140
	i64 u0xad263a63e0f6054b, ; 753: lib_ExoPlayer.Ext.MediaSession.dll.so => 225
	i64 u0xad7e82ed3b0f16d0, ; 754: lib_Xamarin.AndroidX.DocumentFile.dll.so => 261
	i64 u0xad89c07347f1bad6, ; 755: nl/Microsoft.Maui.Controls.resources.dll => 352
	i64 u0xadbb53caf78a79d2, ; 756: System.Web.HttpUtility => 153
	i64 u0xadc90ab061a9e6e4, ; 757: System.ComponentModel.TypeConverter.dll => 17
	i64 u0xadca1b9030b9317e, ; 758: Xamarin.AndroidX.Collection.Ktx => 251
	i64 u0xadd8eda2edf396ad, ; 759: Xamarin.Android.Glide.GifDecoder => 233
	i64 u0xadf4cf30debbeb9a, ; 760: System.Net.ServicePoint.dll => 75
	i64 u0xadf511667bef3595, ; 761: System.Net.Security => 74
	i64 u0xae0aaa94fdcfce0f, ; 762: System.ComponentModel.EventBasedAsync.dll => 15
	i64 u0xae191474c509f750, ; 763: lib_ExoPlayer.Core.dll.so => 218
	i64 u0xae282bcd03739de7, ; 764: Java.Interop => 169
	i64 u0xae53579c90db1107, ; 765: System.ObjectModel.dll => 85
	i64 u0xaec7c0c7e2ed4575, ; 766: lib_Xamarin.KotlinX.AtomicFU.Jvm.dll.so => 326
	i64 u0xaf732d0b2193b8f5, ; 767: System.Security.Cryptography.OpenSsl.dll => 124
	i64 u0xafdb94dbccd9d11c, ; 768: Xamarin.AndroidX.Lifecycle.LiveData.dll => 273
	i64 u0xafe29f45095518e7, ; 769: lib_Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll.so => 284
	i64 u0xb03ae931fb25607e, ; 770: Xamarin.AndroidX.ConstraintLayout => 253
	i64 u0xb05cc42cd94c6d9d, ; 771: lib-sv-Microsoft.Maui.Controls.resources.dll.so => 359
	i64 u0xb0ac21bec8f428c5, ; 772: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.Android.dll => 280
	i64 u0xb0bb43dc52ea59f9, ; 773: System.Diagnostics.Tracing.dll => 34
	i64 u0xb1dd05401aa8ee63, ; 774: System.Security.AccessControl => 118
	i64 u0xb220631954820169, ; 775: System.Text.RegularExpressions => 139
	i64 u0xb2376e1dbf8b4ed7, ; 776: System.Security.Cryptography.Csp => 122
	i64 u0xb2a1959fe95c5402, ; 777: lib_System.Runtime.InteropServices.JavaScript.dll.so => 106
	i64 u0xb3011a0a57f7ffb2, ; 778: Microsoft.VisualStudio.DesignTools.MobileTapContracts.dll => 368
	i64 u0xb3874072ee0ecf8c, ; 779: Xamarin.AndroidX.VectorDrawable.Animated.dll => 305
	i64 u0xb3f0a0fcda8d3ebc, ; 780: Xamarin.AndroidX.CardView => 248
	i64 u0xb4bd7015ecee9d86, ; 781: System.IO.Pipelines => 54
	i64 u0xb4c53d9749c5f226, ; 782: lib_System.IO.FileSystem.AccessControl.dll.so => 47
	i64 u0xb4e255db60ce6292, ; 783: ExoPlayer.Database.dll => 220
	i64 u0xb4ff710863453fda, ; 784: System.Diagnostics.FileVersionInfo.dll => 28
	i64 u0xb540474e6039021a, ; 785: Youth.dll => 0
	i64 u0xb5c38bf497a4cfe2, ; 786: lib_System.Threading.Tasks.dll.so => 145
	i64 u0xb5c7fcdafbc67ee4, ; 787: Microsoft.Extensions.Logging.Abstractions.dll => 187
	i64 u0xb5ea31d5244c6626, ; 788: System.Threading.ThreadPool.dll => 147
	i64 u0xb7212c4683a94afe, ; 789: System.Drawing.Primitives => 35
	i64 u0xb77a6028c2b2a574, ; 790: id\Microsoft.Maui.Controls.resources => 346
	i64 u0xb81a2c6e0aee50fe, ; 791: lib_System.Private.CoreLib.dll.so => 173
	i64 u0xb8b0a9b3dfbc5cb7, ; 792: Xamarin.AndroidX.Window.Extensions.Core.Core => 310
	i64 u0xb8c60af47c08d4da, ; 793: System.Net.ServicePoint => 75
	i64 u0xb8e68d20aad91196, ; 794: lib_System.Xml.XPath.dll.so => 161
	i64 u0xb917fc2f9540e65a, ; 795: ca\Microsoft.Maui.Controls.resources => 334
	i64 u0xb9185c33a1643eed, ; 796: Microsoft.CSharp.dll => 1
	i64 u0xb97d03125eff2f3d, ; 797: ExoPlayer.Hls.dll => 224
	i64 u0xb9b19a3eb1924681, ; 798: lib_Microsoft.Maui.Controls.Maps.dll.so => 197
	i64 u0xb9b8001adf4ed7cc, ; 799: lib_Xamarin.AndroidX.SlidingPaneLayout.dll.so => 299
	i64 u0xb9f64d3b230def68, ; 800: lib-pt-Microsoft.Maui.Controls.resources.dll.so => 355
	i64 u0xba4670aa94a2b3c6, ; 801: lib_System.Xml.XDocument.dll.so => 159
	i64 u0xba48785529705af9, ; 802: System.Collections.dll => 12
	i64 u0xba965b8c86359996, ; 803: lib_System.Windows.dll.so => 155
	i64 u0xbb286883bc35db36, ; 804: System.Transactions.dll => 151
	i64 u0xbb65706fde942ce3, ; 805: System.Net.Sockets => 76
	i64 u0xbb6dc0b35452c1a0, ; 806: ZXing.Net.MAUI.dll => 331
	i64 u0xbba28979413cad9e, ; 807: lib_System.Runtime.CompilerServices.VisualC.dll.so => 103
	i64 u0xbbd180354b67271a, ; 808: System.Runtime.Serialization.Formatters => 112
	i64 u0xbc260cdba33291a3, ; 809: Xamarin.AndroidX.Arch.Core.Common.dll => 241
	i64 u0xbcfd4e14fffb3bb4, ; 810: MvvmHelpers.dll => 212
	i64 u0xbd0e2c0d55246576, ; 811: System.Net.Http.dll => 65
	i64 u0xbd3fbd85b9e1cb29, ; 812: lib_System.Net.HttpListener.dll.so => 66
	i64 u0xbd437a2cdb333d0d, ; 813: Xamarin.AndroidX.ViewPager2 => 308
	i64 u0xbd4f572d2bd0a789, ; 814: System.IO.Compression.ZipFile.dll => 45
	i64 u0xbd5d0b88d3d647a5, ; 815: lib_Xamarin.AndroidX.Browser.dll.so => 243
	i64 u0xbd877b14d0b56392, ; 816: System.Runtime.Intrinsics.dll => 109
	i64 u0xbe08e3083025c53d, ; 817: ZXing.Net.MAUI.Controls.dll => 332
	i64 u0xbe532a80075c3dc8, ; 818: Xamarin.AndroidX.Camera.Core.dll => 245
	i64 u0xbe65a49036345cf4, ; 819: lib_System.Buffers.dll.so => 7
	i64 u0xbee38d4a88835966, ; 820: Xamarin.AndroidX.AppCompat.AppCompatResources => 240
	i64 u0xbef9919db45b4ca7, ; 821: System.IO.Pipes.AccessControl => 55
	i64 u0xbf0fa68611139208, ; 822: lib_Xamarin.AndroidX.Annotation.dll.so => 236
	i64 u0xbfc1e1fb3095f2b3, ; 823: lib_System.Net.Http.Json.dll.so => 64
	i64 u0xc040a4ab55817f58, ; 824: ar/Microsoft.Maui.Controls.resources.dll => 333
	i64 u0xc07cadab29efeba0, ; 825: Xamarin.AndroidX.Core.Core.Ktx.dll => 257
	i64 u0xc0d928351ab5ca77, ; 826: System.Console.dll => 20
	i64 u0xc0f0b74d527b7090, ; 827: CommunityToolkit.Diagnostics.dll => 175
	i64 u0xc0f5a221a9383aea, ; 828: System.Runtime.Intrinsics => 109
	i64 u0xc111030af54d7191, ; 829: System.Resources.Writer => 101
	i64 u0xc12b8b3afa48329c, ; 830: lib_System.Linq.dll.so => 62
	i64 u0xc183ca0b74453aa9, ; 831: lib_System.Threading.Tasks.Dataflow.dll.so => 142
	i64 u0xc1ff9ae3cdb6e1e6, ; 832: Xamarin.AndroidX.Activity.dll => 234
	i64 u0xc26c064effb1dea9, ; 833: System.Buffers.dll => 7
	i64 u0xc278de356ad8a9e3, ; 834: Microsoft.IdentityModel.Logging => 193
	i64 u0xc28c50f32f81cc73, ; 835: ja/Microsoft.Maui.Controls.resources.dll => 348
	i64 u0xc2902f6cf5452577, ; 836: lib_Mono.Android.Export.dll.so => 170
	i64 u0xc2a3bca55b573141, ; 837: System.IO.FileSystem.Watcher => 50
	i64 u0xc2bcfec99f69365e, ; 838: Xamarin.AndroidX.ViewPager2.dll => 308
	i64 u0xc2e2a2238494ffac, ; 839: CommunityToolkit.Common.dll => 174
	i64 u0xc30b52815b58ac2c, ; 840: lib_System.Runtime.Serialization.Xml.dll.so => 115
	i64 u0xc36d7d89c652f455, ; 841: System.Threading.Overlapped => 141
	i64 u0xc396b285e59e5493, ; 842: GoogleGson.dll => 181
	i64 u0xc3c86c1e5e12f03d, ; 843: WindowsBase => 166
	i64 u0xc3f0e03e56ce7b69, ; 844: zxing => 330
	i64 u0xc421b61fd853169d, ; 845: lib_System.Net.WebSockets.Client.dll.so => 80
	i64 u0xc463e077917aa21d, ; 846: System.Runtime.Serialization.Json => 113
	i64 u0xc48d7aeef918363b, ; 847: lib_ExoPlayer.Common.dll.so => 216
	i64 u0xc4d3858ed4d08512, ; 848: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 284
	i64 u0xc50fded0ded1418c, ; 849: lib_System.ComponentModel.TypeConverter.dll.so => 17
	i64 u0xc519125d6bc8fb11, ; 850: lib_System.Net.Requests.dll.so => 73
	i64 u0xc5293b19e4dc230e, ; 851: Xamarin.AndroidX.Navigation.Fragment => 289
	i64 u0xc5325b2fcb37446f, ; 852: lib_System.Private.Xml.dll.so => 89
	i64 u0xc535cb9a21385d9b, ; 853: lib_Xamarin.Android.Glide.DiskLruCache.dll.so => 232
	i64 u0xc5a0f4b95a699af7, ; 854: lib_System.Private.Uri.dll.so => 87
	i64 u0xc5cdcd5b6277579e, ; 855: lib_System.Security.Cryptography.Algorithms.dll.so => 120
	i64 u0xc5ec286825cb0bf4, ; 856: Xamarin.AndroidX.Tracing.Tracing => 302
	i64 u0xc64f6952cef5d09f, ; 857: Microsoft.Maui.Maps.dll => 202
	i64 u0xc6706bc8aa7fe265, ; 858: Xamarin.AndroidX.Annotation.Jvm => 238
	i64 u0xc68e480c8069e1f7, ; 859: Microsoft.Maui.Maps => 202
	i64 u0xc7c01e7d7c93a110, ; 860: System.Text.Encoding.Extensions.dll => 135
	i64 u0xc7ce851898a4548e, ; 861: lib_System.Web.HttpUtility.dll.so => 153
	i64 u0xc809d4089d2556b2, ; 862: System.Runtime.InteropServices.JavaScript.dll => 106
	i64 u0xc858a28d9ee5a6c5, ; 863: lib_System.Collections.Specialized.dll.so => 11
	i64 u0xc8ac7c6bf1c2ec51, ; 864: System.Reflection.DispatchProxy.dll => 90
	i64 u0xc9c62c8f354ac568, ; 865: lib_System.Diagnostics.TextWriterTraceListener.dll.so => 31
	i64 u0xc9e54b32fc19baf3, ; 866: lib_CommunityToolkit.Maui.dll.so => 176
	i64 u0xca3a723e7342c5b6, ; 867: lib-tr-Microsoft.Maui.Controls.resources.dll.so => 361
	i64 u0xca5801070d9fccfb, ; 868: System.Text.Encoding => 136
	i64 u0xcacfddc9f7c6de76, ; 869: ro/Microsoft.Maui.Controls.resources.dll => 356
	i64 u0xcadbc92899a777f0, ; 870: Xamarin.AndroidX.Startup.StartupRuntime => 300
	i64 u0xcb63c799ecf0cf72, ; 871: QRCoder => 211
	i64 u0xcba1cb79f45292b5, ; 872: Xamarin.Android.Glide.GifDecoder.dll => 233
	i64 u0xcbb5f80c7293e696, ; 873: lib_System.Globalization.Calendars.dll.so => 40
	i64 u0xcbd4fdd9cef4a294, ; 874: lib__Microsoft.Android.Resource.Designer.dll.so => 370
	i64 u0xcc15da1e07bbd994, ; 875: Xamarin.AndroidX.SlidingPaneLayout => 299
	i64 u0xcc2876b32ef2794c, ; 876: lib_System.Text.RegularExpressions.dll.so => 139
	i64 u0xcc5c3bb714c4561e, ; 877: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 329
	i64 u0xcc9fa2923aa1c9ef, ; 878: System.Diagnostics.Contracts.dll => 25
	i64 u0xccf25c4b634ccd3a, ; 879: zh-Hans/Microsoft.Maui.Controls.resources.dll => 365
	i64 u0xcd10a42808629144, ; 880: System.Net.Requests => 73
	i64 u0xcdca1b920e9f53ba, ; 881: Xamarin.AndroidX.Interpolator => 269
	i64 u0xcdd0c48b6937b21c, ; 882: Xamarin.AndroidX.SwipeRefreshLayout => 301
	i64 u0xcdd771b5236f4e30, ; 883: lib_PInvoke.Kernel32.dll.so => 206
	i64 u0xcef65cbf0b19b1e3, ; 884: Newtonsoft.Json.Bson => 205
	i64 u0xcf23d8093f3ceadf, ; 885: System.Diagnostics.DiagnosticSource.dll => 27
	i64 u0xcf5ff6b6b2c4c382, ; 886: System.Net.Mail.dll => 67
	i64 u0xcf8fc898f98b0d34, ; 887: System.Private.Xml.Linq => 88
	i64 u0xcfb21487d9cb358b, ; 888: Xamarin.GooglePlayServices.Maps.dll => 321
	i64 u0xd04b5f59ed596e31, ; 889: System.Reflection.Metadata.dll => 95
	i64 u0xd063299fcfc0c93f, ; 890: lib_System.Runtime.Serialization.Json.dll.so => 113
	i64 u0xd07a7cf02bbd9a8c, ; 891: lib_QRCoder.dll.so => 211
	i64 u0xd0de8a113e976700, ; 892: System.Diagnostics.TextWriterTraceListener => 31
	i64 u0xd0fc33d5ae5d4cb8, ; 893: System.Runtime.Extensions => 104
	i64 u0xd105ebba0ad9e275, ; 894: fi\Microsoft.Maui.Controls.resources => 340
	i64 u0xd1194e1d8a8de83c, ; 895: lib_Xamarin.AndroidX.Lifecycle.Common.Jvm.dll.so => 272
	i64 u0xd12beacdfc14f696, ; 896: System.Dynamic.Runtime => 37
	i64 u0xd198e7ce1b6a8344, ; 897: System.Net.Quic.dll => 72
	i64 u0xd23f43eaa6c25c64, ; 898: Xamarin.AndroidX.Media.dll => 287
	i64 u0xd304de876622896e, ; 899: ExoPlayer.Ext.MediaSession => 225
	i64 u0xd3144156a3727ebe, ; 900: Xamarin.Google.Guava.ListenableFuture => 318
	i64 u0xd333d0af9e423810, ; 901: System.Runtime.InteropServices => 108
	i64 u0xd33a415cb4278969, ; 902: System.Security.Cryptography.Encoding.dll => 123
	i64 u0xd3426d966bb704f5, ; 903: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 240
	i64 u0xd3651b6fc3125825, ; 904: System.Private.Uri.dll => 87
	i64 u0xd373685349b1fe8b, ; 905: Microsoft.Extensions.Logging.dll => 186
	i64 u0xd3801faafafb7698, ; 906: System.Private.DataContractSerialization.dll => 86
	i64 u0xd3edcc1f25459a50, ; 907: System.Reflection.Emit => 93
	i64 u0xd4645626dffec99d, ; 908: lib_Microsoft.Extensions.DependencyInjection.Abstractions.dll.so => 185
	i64 u0xd4fa0abb79079ea9, ; 909: System.Security.Principal.dll => 129
	i64 u0xd5507e11a2b2839f, ; 910: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 284
	i64 u0xd567f168deeeaf3c, ; 911: lib_zxing.dll.so => 330
	i64 u0xd5d04bef8478ea19, ; 912: Xamarin.AndroidX.Tracing.Tracing.dll => 302
	i64 u0xd60815f26a12e140, ; 913: Microsoft.Extensions.Logging.Debug.dll => 188
	i64 u0xd618a4a567fb3eaf, ; 914: pl\Microsoft.Maui.Controls.resources => 353
	i64 u0xd63b432ec9306914, ; 915: zxing.dll => 330
	i64 u0xd65786d27a4ad960, ; 916: lib_Microsoft.Maui.Controls.HotReload.Forms.dll.so => 367
	i64 u0xd6694f8359737e4e, ; 917: Xamarin.AndroidX.SavedState => 296
	i64 u0xd6949e129339eae5, ; 918: lib_Xamarin.AndroidX.Core.Core.Ktx.dll.so => 257
	i64 u0xd6d21782156bc35b, ; 919: Xamarin.AndroidX.SwipeRefreshLayout.dll => 301
	i64 u0xd6de019f6af72435, ; 920: Xamarin.AndroidX.ConstraintLayout.Core.dll => 254
	i64 u0xd6f8b724c5400fc8, ; 921: BouncyCastle.Crypto => 210
	i64 u0xd70956d1e6deefb9, ; 922: Jsr305Binding => 313
	i64 u0xd72329819cbbbc44, ; 923: lib_Microsoft.Extensions.Configuration.Abstractions.dll.so => 183
	i64 u0xd72c760af136e863, ; 924: System.Xml.XmlSerializer.dll => 163
	i64 u0xd753f071e44c2a03, ; 925: lib_System.Security.SecureString.dll.so => 130
	i64 u0xd7b3764ada9d341d, ; 926: lib_Microsoft.Extensions.Logging.Abstractions.dll.so => 187
	i64 u0xd7f0088bc5ad71f2, ; 927: Xamarin.AndroidX.VersionedParcelable => 306
	i64 u0xd87a3203b17af395, ; 928: ExoPlayer.Transformer.dll => 228
	i64 u0xd8fb25e28ae30a12, ; 929: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 293
	i64 u0xd9d04d95a2671e29, ; 930: lib_ZXing.Net.MAUI.Controls.dll.so => 332
	i64 u0xda1dfa4c534a9251, ; 931: Microsoft.Extensions.DependencyInjection => 184
	i64 u0xdad05a11827959a3, ; 932: System.Collections.NonGeneric.dll => 10
	i64 u0xdaefdfe71aa53cf9, ; 933: System.IO.FileSystem.Primitives => 49
	i64 u0xdb1c2595127ebf18, ; 934: ExoPlayer.Dash.dll => 219
	i64 u0xdb5383ab5865c007, ; 935: lib-vi-Microsoft.Maui.Controls.resources.dll.so => 363
	i64 u0xdb58816721c02a59, ; 936: lib_System.Reflection.Emit.ILGeneration.dll.so => 91
	i64 u0xdbeda89f832aa805, ; 937: vi/Microsoft.Maui.Controls.resources.dll => 363
	i64 u0xdbf2a779fbc3ac31, ; 938: System.Transactions.Local.dll => 150
	i64 u0xdbf9607a441b4505, ; 939: System.Linq => 62
	i64 u0xdbfc90157a0de9b0, ; 940: lib_System.Text.Encoding.dll.so => 136
	i64 u0xdc75032002d1a212, ; 941: lib_System.Transactions.Local.dll.so => 150
	i64 u0xdca8be7403f92d4f, ; 942: lib_System.Linq.Queryable.dll.so => 61
	i64 u0xdce2c53525640bf3, ; 943: Microsoft.Extensions.Logging => 186
	i64 u0xdd2b722d78ef5f43, ; 944: System.Runtime.dll => 117
	i64 u0xdd67031857c72f96, ; 945: lib_System.Text.Encodings.Web.dll.so => 137
	i64 u0xdd92e229ad292030, ; 946: System.Numerics.dll => 84
	i64 u0xdddcdd701e911af1, ; 947: lib_Xamarin.AndroidX.Legacy.Support.Core.Utils.dll.so => 270
	i64 u0xdde30e6b77aa6f6c, ; 948: lib-zh-Hans-Microsoft.Maui.Controls.resources.dll.so => 365
	i64 u0xde110ae80fa7c2e2, ; 949: System.Xml.XDocument.dll => 159
	i64 u0xde4726fcdf63a198, ; 950: Xamarin.AndroidX.Transition => 303
	i64 u0xde572c2b2fb32f93, ; 951: lib_System.Threading.Tasks.Extensions.dll.so => 143
	i64 u0xde8769ebda7d8647, ; 952: hr/Microsoft.Maui.Controls.resources.dll => 344
	i64 u0xdee075f3477ef6be, ; 953: Xamarin.AndroidX.ExifInterface.dll => 266
	i64 u0xdf4b773de8fb1540, ; 954: System.Net.dll => 82
	i64 u0xdfa254ebb4346068, ; 955: System.Net.Ping => 70
	i64 u0xe0142572c095a480, ; 956: Xamarin.AndroidX.AppCompat.dll => 239
	i64 u0xe021eaa401792a05, ; 957: System.Text.Encoding.dll => 136
	i64 u0xe02f89350ec78051, ; 958: Xamarin.AndroidX.CoordinatorLayout.dll => 255
	i64 u0xe0496b9d65ef5474, ; 959: Xamarin.Android.Glide.DiskLruCache.dll => 232
	i64 u0xe0f282c49dd6bd0a, ; 960: Xamarin.AndroidX.Media => 287
	i64 u0xe10b760bb1462e7a, ; 961: lib_System.Security.Cryptography.Primitives.dll.so => 125
	i64 u0xe1566bbdb759c5af, ; 962: Microsoft.Maui.Controls.HotReload.Forms.dll => 367
	i64 u0xe192a588d4410686, ; 963: lib_System.IO.Pipelines.dll.so => 54
	i64 u0xe1a08bd3fa539e0d, ; 964: System.Runtime.Loader => 110
	i64 u0xe1a77eb8831f7741, ; 965: System.Security.SecureString.dll => 130
	i64 u0xe1ace591eebb7293, ; 966: lib_Newtonsoft.Json.Bson.dll.so => 205
	i64 u0xe1b52f9f816c70ef, ; 967: System.Private.Xml.Linq.dll => 88
	i64 u0xe1e199c8ab02e356, ; 968: System.Data.DataSetExtensions.dll => 23
	i64 u0xe1e7ed1a6e5f9c0e, ; 969: lib_ExoPlayer.Extractor.dll.so => 223
	i64 u0xe1eb30a561a64dfe, ; 970: PInvoke.Windows.ShellScalingApi => 209
	i64 u0xe1ecfdb7fff86067, ; 971: System.Net.Security.dll => 74
	i64 u0xe2252a80fe853de4, ; 972: lib_System.Security.Principal.dll.so => 129
	i64 u0xe22fa4c9c645db62, ; 973: System.Diagnostics.TextWriterTraceListener.dll => 31
	i64 u0xe2420585aeceb728, ; 974: System.Net.Requests.dll => 73
	i64 u0xe26692647e6bcb62, ; 975: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 279
	i64 u0xe29b73bc11392966, ; 976: lib-id-Microsoft.Maui.Controls.resources.dll.so => 346
	i64 u0xe2ad448dee50fbdf, ; 977: System.Xml.Serialization => 158
	i64 u0xe2d920f978f5d85c, ; 978: System.Data.DataSetExtensions => 23
	i64 u0xe2e426c7714fa0bc, ; 979: Microsoft.Win32.Primitives.dll => 4
	i64 u0xe332bacb3eb4a806, ; 980: Mono.Android.Export.dll => 170
	i64 u0xe3811d68d4fe8463, ; 981: pt-BR/Microsoft.Maui.Controls.resources.dll => 354
	i64 u0xe3b7cbae5ad66c75, ; 982: lib_System.Security.Cryptography.Encoding.dll.so => 123
	i64 u0xe494f7ced4ecd10a, ; 983: hu/Microsoft.Maui.Controls.resources.dll => 345
	i64 u0xe4a9b1e40d1e8917, ; 984: lib-fi-Microsoft.Maui.Controls.resources.dll.so => 340
	i64 u0xe4f74a0b5bf9703f, ; 985: System.Runtime.Serialization.Primitives => 114
	i64 u0xe5434e8a119ceb69, ; 986: lib_Mono.Android.dll.so => 172
	i64 u0xe55703b9ce5c038a, ; 987: System.Diagnostics.Tools => 32
	i64 u0xe57013c8afc270b5, ; 988: Microsoft.VisualBasic => 3
	i64 u0xe62913cc36bc07ec, ; 989: System.Xml.dll => 164
	i64 u0xe662f5863d169331, ; 990: ExoPlayer.Rtsp.dll => 226
	i64 u0xe6fb6bcbcc0db67a, ; 991: lib_ExoPlayer.Rtsp.dll.so => 226
	i64 u0xe7bea09c4900a191, ; 992: Xamarin.AndroidX.VectorDrawable.dll => 304
	i64 u0xe7bf2fdfe9685fff, ; 993: PInvoke.Windows.ShellScalingApi.dll => 209
	i64 u0xe7e03cc18dcdeb49, ; 994: lib_System.Diagnostics.StackTrace.dll.so => 30
	i64 u0xe7e147ff99a7a380, ; 995: lib_System.Configuration.dll.so => 19
	i64 u0xe86b0df4ba9e5db8, ; 996: lib_Xamarin.AndroidX.Lifecycle.Runtime.Android.dll.so => 278
	i64 u0xe896622fe0902957, ; 997: System.Reflection.Emit.dll => 93
	i64 u0xe89a2a9ef110899b, ; 998: System.Drawing.dll => 36
	i64 u0xe8c5f8c100b5934b, ; 999: Microsoft.Win32.Registry => 5
	i64 u0xe8efe6c2171f7cd2, ; 1000: Xamarin.Google.Guava.dll => 316
	i64 u0xe957c3976986ab72, ; 1001: lib_Xamarin.AndroidX.Window.Extensions.Core.Core.dll.so => 310
	i64 u0xe98163eb702ae5c5, ; 1002: Xamarin.AndroidX.Arch.Core.Runtime => 242
	i64 u0xe994f23ba4c143e5, ; 1003: Xamarin.KotlinX.Coroutines.Android => 327
	i64 u0xe9b9c8c0458fd92a, ; 1004: System.Windows => 155
	i64 u0xe9d166d87a7f2bdb, ; 1005: lib_Xamarin.AndroidX.Startup.StartupRuntime.dll.so => 300
	i64 u0xea5a4efc2ad81d1b, ; 1006: Xamarin.Google.ErrorProne.Annotations => 315
	i64 u0xea93a785ccb58e40, ; 1007: lib_ExoPlayer.Dash.dll.so => 219
	i64 u0xeb2313fe9d65b785, ; 1008: Xamarin.AndroidX.ConstraintLayout.dll => 253
	i64 u0xeb6e275e78cb8d42, ; 1009: Xamarin.AndroidX.LocalBroadcastManager.dll => 286
	i64 u0xece6cc6c8e0bb0bb, ; 1010: it\Microsoft.Maui.Controls.resources => 347
	i64 u0xed19c616b3fcb7eb, ; 1011: Xamarin.AndroidX.VersionedParcelable.dll => 306
	i64 u0xed60c6fa891c051a, ; 1012: lib_Microsoft.VisualStudio.DesignTools.TapContract.dll.so => 369
	i64 u0xedc4817167106c23, ; 1013: System.Net.Sockets.dll => 76
	i64 u0xedc632067fb20ff3, ; 1014: System.Memory.dll => 63
	i64 u0xedc8e4ca71a02a8b, ; 1015: Xamarin.AndroidX.Navigation.Runtime.dll => 290
	i64 u0xee27c952ed6d058b, ; 1016: Microsoft.Maui.Controls.Maps => 197
	i64 u0xee81f5b3f1c4f83b, ; 1017: System.Threading.ThreadPool => 147
	i64 u0xeeb7ebb80150501b, ; 1018: lib_Xamarin.AndroidX.Collection.Jvm.dll.so => 250
	i64 u0xeefc635595ef57f0, ; 1019: System.Security.Cryptography.Cng => 121
	i64 u0xef03b1b5a04e9709, ; 1020: System.Text.Encoding.CodePages.dll => 134
	i64 u0xef432781d5667f61, ; 1021: Xamarin.AndroidX.Print => 292
	i64 u0xef602c523fe2e87a, ; 1022: lib_Xamarin.Google.Guava.ListenableFuture.dll.so => 318
	i64 u0xef72742e1bcca27a, ; 1023: Microsoft.Maui.Essentials.dll => 200
	i64 u0xefd1e0c4e5c9b371, ; 1024: System.Resources.ResourceManager.dll => 100
	i64 u0xefe8f8d5ed3c72ea, ; 1025: System.Formats.Tar.dll => 39
	i64 u0xefec0b7fdc57ec42, ; 1026: Xamarin.AndroidX.Activity => 234
	i64 u0xf00c29406ea45e19, ; 1027: es/Microsoft.Maui.Controls.resources.dll => 339
	i64 u0xf09e47b6ae914f6e, ; 1028: System.Net.NameResolution => 68
	i64 u0xf0ac2b489fed2e35, ; 1029: lib_System.Diagnostics.Debug.dll.so => 26
	i64 u0xf0bb49dadd3a1fe1, ; 1030: lib_System.Net.ServicePoint.dll.so => 75
	i64 u0xf0bd25bd94a833d5, ; 1031: ExoPlayer.Extractor.dll => 223
	i64 u0xf0ce884378140f58, ; 1032: Validation => 214
	i64 u0xf0de2537ee19c6ca, ; 1033: lib_System.Net.WebHeaderCollection.dll.so => 78
	i64 u0xf10c376a24899675, ; 1034: lib_ExoPlayer.Hls.dll.so => 224
	i64 u0xf1138779fa181c68, ; 1035: lib_Xamarin.AndroidX.Lifecycle.Runtime.dll.so => 277
	i64 u0xf11b621fc87b983f, ; 1036: Microsoft.Maui.Controls.Xaml.dll => 198
	i64 u0xf161f4f3c3b7e62c, ; 1037: System.Data => 24
	i64 u0xf16eb650d5a464bc, ; 1038: System.ValueTuple => 152
	i64 u0xf1c4b4005493d871, ; 1039: System.Formats.Asn1.dll => 38
	i64 u0xf238bd79489d3a96, ; 1040: lib-nl-Microsoft.Maui.Controls.resources.dll.so => 352
	i64 u0xf260dbb5f9e31174, ; 1041: CommunityToolkit.Maui.MediaElement.dll => 179
	i64 u0xf2feea356ba760af, ; 1042: Xamarin.AndroidX.Arch.Core.Runtime.dll => 242
	i64 u0xf300e085f8acd238, ; 1043: lib_System.ServiceProcess.dll.so => 133
	i64 u0xf34e52b26e7e059d, ; 1044: System.Runtime.CompilerServices.VisualC.dll => 103
	i64 u0xf37221fda4ef8830, ; 1045: lib_Xamarin.Google.Android.Material.dll.so => 311
	i64 u0xf3ad9b8fb3eefd12, ; 1046: lib_System.IO.UnmanagedMemoryStream.dll.so => 57
	i64 u0xf3ddfe05336abf29, ; 1047: System => 165
	i64 u0xf408654b2a135055, ; 1048: System.Reflection.Emit.ILGeneration.dll => 91
	i64 u0xf4103170a1de5bd0, ; 1049: System.Linq.Queryable.dll => 61
	i64 u0xf42d20c23173d77c, ; 1050: lib_System.ServiceModel.Web.dll.so => 132
	i64 u0xf4c1dd70a5496a17, ; 1051: System.IO.Compression => 46
	i64 u0xf4ecf4b9afc64781, ; 1052: System.ServiceProcess.dll => 133
	i64 u0xf4eeeaa566e9b970, ; 1053: lib_Xamarin.AndroidX.CustomView.PoolingContainer.dll.so => 260
	i64 u0xf518f63ead11fcd1, ; 1054: System.Threading.Tasks => 145
	i64 u0xf55749f30d565f62, ; 1055: nb\Microsoft.Maui.Controls.resources => 351
	i64 u0xf5fc7602fe27b333, ; 1056: System.Net.WebHeaderCollection => 78
	i64 u0xf6077741019d7428, ; 1057: Xamarin.AndroidX.CoordinatorLayout => 255
	i64 u0xf61ade9836ad4692, ; 1058: Microsoft.IdentityModel.Tokens.dll => 194
	i64 u0xf6742cbf457c450b, ; 1059: Xamarin.AndroidX.Lifecycle.Runtime.Android.dll => 278
	i64 u0xf6c0e7d55a7a4e4f, ; 1060: Microsoft.IdentityModel.JsonWebTokens => 192
	i64 u0xf70c0a7bf8ccf5af, ; 1061: System.Web => 154
	i64 u0xf75e91e8798fcc03, ; 1062: es\Microsoft.Maui.Controls.resources => 339
	i64 u0xf77b20923f07c667, ; 1063: de/Microsoft.Maui.Controls.resources.dll => 337
	i64 u0xf7d6224976ea4d7d, ; 1064: ExoPlayer.Extractor => 223
	i64 u0xf7e2cac4c45067b3, ; 1065: lib_System.Numerics.Vectors.dll.so => 83
	i64 u0xf7e74930e0e3d214, ; 1066: zh-HK/Microsoft.Maui.Controls.resources.dll => 364
	i64 u0xf7fa0bf77fe677cc, ; 1067: Newtonsoft.Json.dll => 204
	i64 u0xf834629265533ade, ; 1068: hu\Microsoft.Maui.Controls.resources => 345
	i64 u0xf84773b5c81e3cef, ; 1069: lib-uk-Microsoft.Maui.Controls.resources.dll.so => 362
	i64 u0xf8aac5ea82de1348, ; 1070: System.Linq.Queryable => 61
	i64 u0xf8abd63acd77d37b, ; 1071: Xamarin.AndroidX.Camera.View => 247
	i64 u0xf8b77539b362d3ba, ; 1072: lib_System.Reflection.Primitives.dll.so => 96
	i64 u0xf8e045dc345b2ea3, ; 1073: lib_Xamarin.AndroidX.RecyclerView.dll.so => 294
	i64 u0xf915dc29808193a1, ; 1074: System.Web.HttpUtility.dll => 153
	i64 u0xf96c777a2a0686f4, ; 1075: hi/Microsoft.Maui.Controls.resources.dll => 343
	i64 u0xf9be54c8bcf8ff3b, ; 1076: System.Security.AccessControl.dll => 118
	i64 u0xf9eec5bb3a6aedc6, ; 1077: Microsoft.Extensions.Options => 189
	i64 u0xfa0e82300e67f913, ; 1078: lib_System.AppContext.dll.so => 6
	i64 u0xfa2fdb27e8a2c8e8, ; 1079: System.ComponentModel.EventBasedAsync => 15
	i64 u0xfa3f278f288b0e84, ; 1080: lib_System.Net.Security.dll.so => 74
	i64 u0xfa5754ddda6c4b4e, ; 1081: lib_ExoPlayer.Decoder.dll.so => 222
	i64 u0xfa5ed7226d978949, ; 1082: lib-ar-Microsoft.Maui.Controls.resources.dll.so => 333
	i64 u0xfa645d91e9fc4cba, ; 1083: System.Threading.Thread => 146
	i64 u0xfa96f06b9953ab91, ; 1084: lib_ExoPlayer.Transformer.dll.so => 228
	i64 u0xfad4d2c770e827f9, ; 1085: lib_System.IO.IsolatedStorage.dll.so => 52
	i64 u0xfb06dd2338e6f7c4, ; 1086: System.Net.Ping.dll => 70
	i64 u0xfb087abe5365e3b7, ; 1087: lib_System.Data.DataSetExtensions.dll.so => 23
	i64 u0xfb846e949baff5ea, ; 1088: System.Xml.Serialization.dll => 158
	i64 u0xfbad3e4ce4b98145, ; 1089: System.Security.Cryptography.X509Certificates => 126
	i64 u0xfbf0a31c9fc34bc4, ; 1090: lib_System.Net.Http.dll.so => 65
	i64 u0xfc61ddcf78dd1f54, ; 1091: Xamarin.AndroidX.LocalBroadcastManager => 286
	i64 u0xfc6b7527cc280b3f, ; 1092: lib_System.Runtime.Serialization.Formatters.dll.so => 112
	i64 u0xfc719aec26adf9d9, ; 1093: Xamarin.AndroidX.Navigation.Fragment.dll => 289
	i64 u0xfc82690c2fe2735c, ; 1094: Xamarin.AndroidX.Lifecycle.Process.dll => 276
	i64 u0xfc93fc307d279893, ; 1095: System.IO.Pipes.AccessControl.dll => 55
	i64 u0xfcd302092ada6328, ; 1096: System.IO.MemoryMappedFiles.dll => 53
	i64 u0xfd22f00870e40ae0, ; 1097: lib_Xamarin.AndroidX.DrawerLayout.dll.so => 262
	i64 u0xfd49b3c1a76e2748, ; 1098: System.Runtime.InteropServices.RuntimeInformation => 107
	i64 u0xfd50b45aad5dfa23, ; 1099: lib_ExoPlayer.SmoothStreaming.dll.so => 227
	i64 u0xfd536c702f64dc47, ; 1100: System.Text.Encoding.Extensions => 135
	i64 u0xfd583f7657b6a1cb, ; 1101: Xamarin.AndroidX.Fragment => 267
	i64 u0xfd8dd91a2c26bd5d, ; 1102: Xamarin.AndroidX.Lifecycle.Runtime => 277
	i64 u0xfda36abccf05cf5c, ; 1103: System.Net.WebSockets.Client => 80
	i64 u0xfdbe4710aa9beeff, ; 1104: CommunityToolkit.Maui => 176
	i64 u0xfdbff69521edd1c7, ; 1105: PInvoke.User32.dll => 207
	i64 u0xfddbe9695626a7f5, ; 1106: Xamarin.AndroidX.Lifecycle.Common => 271
	i64 u0xfe30a99332f3d503, ; 1107: CommunityToolkit.Maui.MediaElement => 179
	i64 u0xfebe1950717515f9, ; 1108: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 275
	i64 u0xff270a55858bac8d, ; 1109: System.Security.Principal => 129
	i64 u0xff9b54613e0d2cc8, ; 1110: System.Net.Http.Json => 64
	i64 u0xffd56baf9bdab829, ; 1111: ro\Microsoft.Maui.Controls.resources => 356
	i64 u0xffdb7a971be4ec73 ; 1112: System.ValueTuple.dll => 152
], align 8

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [1113 x i32] [
	i32 42, i32 328, i32 301, i32 206, i32 13, i32 229, i32 331, i32 290,
	i32 177, i32 0, i32 105, i32 171, i32 48, i32 239, i32 7, i32 86,
	i32 335, i32 191, i32 263, i32 224, i32 71, i32 322, i32 294, i32 12,
	i32 199, i32 102, i32 206, i32 156, i32 19, i32 268, i32 250, i32 161,
	i32 265, i32 304, i32 167, i32 216, i32 357, i32 10, i32 188, i32 305,
	i32 96, i32 260, i32 262, i32 13, i32 189, i32 10, i32 320, i32 127,
	i32 95, i32 140, i32 218, i32 39, i32 307, i32 246, i32 321, i32 172,
	i32 233, i32 5, i32 200, i32 67, i32 298, i32 130, i32 297, i32 366,
	i32 264, i32 68, i32 251, i32 221, i32 66, i32 57, i32 259, i32 52,
	i32 344, i32 43, i32 125, i32 244, i32 67, i32 81, i32 279, i32 369,
	i32 158, i32 92, i32 99, i32 294, i32 141, i32 227, i32 151, i32 243,
	i32 341, i32 162, i32 169, i32 185, i32 81, i32 369, i32 251, i32 4,
	i32 5, i32 51, i32 101, i32 218, i32 363, i32 216, i32 174, i32 56,
	i32 210, i32 120, i32 98, i32 168, i32 118, i32 359, i32 328, i32 21,
	i32 345, i32 137, i32 97, i32 77, i32 351, i32 292, i32 300, i32 119,
	i32 207, i32 247, i32 8, i32 165, i32 360, i32 70, i32 232, i32 280,
	i32 295, i32 171, i32 145, i32 40, i32 195, i32 298, i32 47, i32 30,
	i32 317, i32 215, i32 291, i32 349, i32 144, i32 189, i32 163, i32 28,
	i32 84, i32 302, i32 77, i32 43, i32 228, i32 219, i32 29, i32 42,
	i32 103, i32 117, i32 237, i32 45, i32 91, i32 56, i32 148, i32 368,
	i32 320, i32 146, i32 100, i32 49, i32 227, i32 20, i32 256, i32 114,
	i32 230, i32 212, i32 341, i32 314, i32 244, i32 324, i32 190, i32 245,
	i32 94, i32 58, i32 213, i32 346, i32 361, i32 81, i32 214, i32 314,
	i32 169, i32 26, i32 364, i32 71, i32 293, i32 266, i32 367, i32 362,
	i32 69, i32 33, i32 14, i32 139, i32 213, i32 38, i32 175, i32 366,
	i32 252, i32 353, i32 134, i32 92, i32 88, i32 197, i32 149, i32 317,
	i32 359, i32 24, i32 138, i32 57, i32 51, i32 338, i32 337, i32 29,
	i32 157, i32 34, i32 164, i32 267, i32 191, i32 202, i32 52, i32 370,
	i32 309, i32 90, i32 248, i32 35, i32 341, i32 157, i32 357, i32 9,
	i32 339, i32 333, i32 209, i32 76, i32 55, i32 199, i32 335, i32 196,
	i32 13, i32 308, i32 182, i32 241, i32 109, i32 362, i32 283, i32 32,
	i32 104, i32 84, i32 92, i32 53, i32 96, i32 323, i32 58, i32 9,
	i32 102, i32 343, i32 259, i32 68, i32 307, i32 334, i32 204, i32 125,
	i32 295, i32 116, i32 135, i32 194, i32 126, i32 106, i32 324, i32 131,
	i32 243, i32 332, i32 318, i32 147, i32 156, i32 268, i32 256, i32 263,
	i32 295, i32 97, i32 24, i32 299, i32 143, i32 352, i32 292, i32 288,
	i32 3, i32 214, i32 167, i32 240, i32 100, i32 161, i32 99, i32 25,
	i32 93, i32 168, i32 172, i32 235, i32 3, i32 353, i32 265, i32 1,
	i32 114, i32 324, i32 268, i32 276, i32 213, i32 33, i32 6, i32 349,
	i32 357, i32 156, i32 220, i32 53, i32 270, i32 85, i32 306, i32 291,
	i32 44, i32 275, i32 104, i32 220, i32 47, i32 138, i32 217, i32 64,
	i32 321, i32 285, i32 69, i32 80, i32 59, i32 89, i32 154, i32 241,
	i32 133, i32 110, i32 195, i32 347, i32 285, i32 221, i32 293, i32 171,
	i32 134, i32 140, i32 40, i32 211, i32 175, i32 338, i32 194, i32 196,
	i32 312, i32 60, i32 180, i32 282, i32 79, i32 25, i32 36, i32 99,
	i32 279, i32 71, i32 22, i32 256, i32 201, i32 358, i32 229, i32 121,
	i32 69, i32 107, i32 364, i32 286, i32 119, i32 117, i32 271, i32 212,
	i32 221, i32 272, i32 11, i32 2, i32 124, i32 115, i32 142, i32 41,
	i32 87, i32 317, i32 236, i32 173, i32 27, i32 148, i32 348, i32 184,
	i32 315, i32 235, i32 1, i32 226, i32 237, i32 44, i32 255, i32 149,
	i32 350, i32 270, i32 18, i32 316, i32 86, i32 336, i32 319, i32 41,
	i32 275, i32 249, i32 208, i32 280, i32 94, i32 186, i32 28, i32 41,
	i32 78, i32 331, i32 264, i32 252, i32 144, i32 108, i32 250, i32 203,
	i32 11, i32 105, i32 137, i32 16, i32 122, i32 66, i32 157, i32 22,
	i32 338, i32 102, i32 184, i32 329, i32 63, i32 58, i32 198, i32 110,
	i32 173, i32 316, i32 327, i32 9, i32 311, i32 120, i32 98, i32 105,
	i32 283, i32 176, i32 196, i32 111, i32 354, i32 238, i32 365, i32 49,
	i32 20, i32 282, i32 258, i32 72, i32 254, i32 155, i32 39, i32 336,
	i32 35, i32 325, i32 38, i32 342, i32 178, i32 310, i32 178, i32 108,
	i32 21, i32 323, i32 281, i32 201, i32 15, i32 190, i32 79, i32 79,
	i32 258, i32 190, i32 261, i32 289, i32 297, i32 152, i32 21, i32 199,
	i32 50, i32 51, i32 287, i32 361, i32 351, i32 94, i32 231, i32 347,
	i32 16, i32 123, i32 344, i32 160, i32 45, i32 315, i32 181, i32 116,
	i32 63, i32 166, i32 182, i32 14, i32 296, i32 207, i32 111, i32 238,
	i32 60, i32 326, i32 312, i32 121, i32 350, i32 2, i32 360, i32 267,
	i32 281, i32 179, i32 325, i32 281, i32 6, i32 249, i32 340, i32 263,
	i32 336, i32 192, i32 17, i32 358, i32 337, i32 77, i32 253, i32 335,
	i32 312, i32 320, i32 177, i32 131, i32 323, i32 350, i32 83, i32 188,
	i32 12, i32 34, i32 119, i32 276, i32 265, i32 85, i32 230, i32 322,
	i32 18, i32 307, i32 183, i32 274, i32 217, i32 72, i32 368, i32 95,
	i32 165, i32 269, i32 82, i32 239, i32 248, i32 326, i32 154, i32 36,
	i32 151, i32 191, i32 247, i32 144, i32 0, i32 56, i32 113, i32 249,
	i32 304, i32 303, i32 37, i32 366, i32 182, i32 115, i32 215, i32 174,
	i32 237, i32 14, i32 231, i32 146, i32 43, i32 200, i32 235, i32 98,
	i32 329, i32 168, i32 16, i32 48, i32 107, i32 97, i32 285, i32 27,
	i32 128, i32 29, i32 342, i32 297, i32 128, i32 44, i32 258, i32 264,
	i32 149, i32 8, i32 342, i32 204, i32 288, i32 343, i32 360, i32 356,
	i32 322, i32 355, i32 132, i32 354, i32 42, i32 33, i32 370, i32 46,
	i32 143, i32 282, i32 229, i32 198, i32 273, i32 259, i32 138, i32 62,
	i32 178, i32 132, i32 334, i32 48, i32 160, i32 242, i32 273, i32 231,
	i32 271, i32 203, i32 303, i32 46, i32 164, i32 193, i32 269, i32 348,
	i32 222, i32 193, i32 244, i32 246, i32 266, i32 201, i32 319, i32 18,
	i32 8, i32 181, i32 257, i32 124, i32 245, i32 59, i32 195, i32 141,
	i32 180, i32 290, i32 349, i32 277, i32 313, i32 309, i32 150, i32 142,
	i32 328, i32 325, i32 126, i32 327, i32 217, i32 160, i32 205, i32 162,
	i32 260, i32 234, i32 183, i32 208, i32 26, i32 288, i32 210, i32 274,
	i32 246, i32 82, i32 309, i32 127, i32 314, i32 101, i32 148, i32 311,
	i32 291, i32 54, i32 162, i32 167, i32 131, i32 37, i32 305, i32 180,
	i32 203, i32 22, i32 358, i32 112, i32 90, i32 261, i32 50, i32 60,
	i32 122, i32 83, i32 127, i32 163, i32 313, i32 166, i32 296, i32 215,
	i32 298, i32 262, i32 355, i32 230, i32 278, i32 4, i32 272, i32 225,
	i32 170, i32 2, i32 319, i32 283, i32 116, i32 192, i32 236, i32 208,
	i32 19, i32 177, i32 187, i32 89, i32 65, i32 30, i32 185, i32 254,
	i32 59, i32 222, i32 111, i32 274, i32 32, i32 128, i32 159, i32 252,
	i32 140, i32 225, i32 261, i32 352, i32 153, i32 17, i32 251, i32 233,
	i32 75, i32 74, i32 15, i32 218, i32 169, i32 85, i32 326, i32 124,
	i32 273, i32 284, i32 253, i32 359, i32 280, i32 34, i32 118, i32 139,
	i32 122, i32 106, i32 368, i32 305, i32 248, i32 54, i32 47, i32 220,
	i32 28, i32 0, i32 145, i32 187, i32 147, i32 35, i32 346, i32 173,
	i32 310, i32 75, i32 161, i32 334, i32 1, i32 224, i32 197, i32 299,
	i32 355, i32 159, i32 12, i32 155, i32 151, i32 76, i32 331, i32 103,
	i32 112, i32 241, i32 212, i32 65, i32 66, i32 308, i32 45, i32 243,
	i32 109, i32 332, i32 245, i32 7, i32 240, i32 55, i32 236, i32 64,
	i32 333, i32 257, i32 20, i32 175, i32 109, i32 101, i32 62, i32 142,
	i32 234, i32 7, i32 193, i32 348, i32 170, i32 50, i32 308, i32 174,
	i32 115, i32 141, i32 181, i32 166, i32 330, i32 80, i32 113, i32 216,
	i32 284, i32 17, i32 73, i32 289, i32 89, i32 232, i32 87, i32 120,
	i32 302, i32 202, i32 238, i32 202, i32 135, i32 153, i32 106, i32 11,
	i32 90, i32 31, i32 176, i32 361, i32 136, i32 356, i32 300, i32 211,
	i32 233, i32 40, i32 370, i32 299, i32 139, i32 329, i32 25, i32 365,
	i32 73, i32 269, i32 301, i32 206, i32 205, i32 27, i32 67, i32 88,
	i32 321, i32 95, i32 113, i32 211, i32 31, i32 104, i32 340, i32 272,
	i32 37, i32 72, i32 287, i32 225, i32 318, i32 108, i32 123, i32 240,
	i32 87, i32 186, i32 86, i32 93, i32 185, i32 129, i32 284, i32 330,
	i32 302, i32 188, i32 353, i32 330, i32 367, i32 296, i32 257, i32 301,
	i32 254, i32 210, i32 313, i32 183, i32 163, i32 130, i32 187, i32 306,
	i32 228, i32 293, i32 332, i32 184, i32 10, i32 49, i32 219, i32 363,
	i32 91, i32 363, i32 150, i32 62, i32 136, i32 150, i32 61, i32 186,
	i32 117, i32 137, i32 84, i32 270, i32 365, i32 159, i32 303, i32 143,
	i32 344, i32 266, i32 82, i32 70, i32 239, i32 136, i32 255, i32 232,
	i32 287, i32 125, i32 367, i32 54, i32 110, i32 130, i32 205, i32 88,
	i32 23, i32 223, i32 209, i32 74, i32 129, i32 31, i32 73, i32 279,
	i32 346, i32 158, i32 23, i32 4, i32 170, i32 354, i32 123, i32 345,
	i32 340, i32 114, i32 172, i32 32, i32 3, i32 164, i32 226, i32 226,
	i32 304, i32 209, i32 30, i32 19, i32 278, i32 93, i32 36, i32 5,
	i32 316, i32 310, i32 242, i32 327, i32 155, i32 300, i32 315, i32 219,
	i32 253, i32 286, i32 347, i32 306, i32 369, i32 76, i32 63, i32 290,
	i32 197, i32 147, i32 250, i32 121, i32 134, i32 292, i32 318, i32 200,
	i32 100, i32 39, i32 234, i32 339, i32 68, i32 26, i32 75, i32 223,
	i32 214, i32 78, i32 224, i32 277, i32 198, i32 24, i32 152, i32 38,
	i32 352, i32 179, i32 242, i32 133, i32 103, i32 311, i32 57, i32 165,
	i32 91, i32 61, i32 132, i32 46, i32 133, i32 260, i32 145, i32 351,
	i32 78, i32 255, i32 194, i32 278, i32 192, i32 154, i32 339, i32 337,
	i32 223, i32 83, i32 364, i32 204, i32 345, i32 362, i32 61, i32 247,
	i32 96, i32 294, i32 153, i32 343, i32 118, i32 189, i32 6, i32 15,
	i32 74, i32 222, i32 333, i32 146, i32 228, i32 52, i32 70, i32 23,
	i32 158, i32 126, i32 65, i32 286, i32 112, i32 289, i32 276, i32 55,
	i32 53, i32 262, i32 107, i32 227, i32 135, i32 267, i32 277, i32 80,
	i32 176, i32 207, i32 271, i32 179, i32 275, i32 129, i32 64, i32 356,
	i32 152
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 8

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 8

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 u0x0000000000000000, ; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 8

; Functions

; Function attributes: memory(write, argmem: none, inaccessiblemem: none) "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 8, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { memory(write, argmem: none, inaccessiblemem: none) "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+fix-cortex-a53-835769,+neon,+outline-atomics,+v8a" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+fix-cortex-a53-835769,+neon,+outline-atomics,+v8a" }

; Metadata
!llvm.module.flags = !{!0, !1, !7, !8, !9, !10}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!".NET for Android remotes/origin/release/9.0.1xx-rc1 @ 48e3ff420b194f8cd8ba5652574f35272b71efaa"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"branch-target-enforcement", i32 0}
!8 = !{i32 1, !"sign-return-address", i32 0}
!9 = !{i32 1, !"sign-return-address-all", i32 0}
!10 = !{i32 1, !"sign-return-address-with-bkey", i32 0}
