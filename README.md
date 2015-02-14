##YuYingjian.Crypto
.Net的加密库0.0.1版。

visual studio 2013编写 .net framework 4.5.1

目前只是实现了DES和AES加密算法，并作为string上的扩展方法。

###Example
```cs
using YuYingjian.Crypto;

//下面的代码用于自己设定key和iv
DESCrypto.SetKeyAndIV(key=key, iv=iv); //key必须能转成至少8字节，iv8字节
AESCrypto.SetKeyAndIV(key=key, iv=iv); //key必须能转成至少32字节，iv16字节

"虞颖健".ToDES().ToAES().FromAES().FromDES(); //虞颖健
```

###NuGet
PM> Install-Package YuYingjian.Crypto
