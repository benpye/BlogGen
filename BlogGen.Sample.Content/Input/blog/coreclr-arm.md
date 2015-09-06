```
@Gen.Properties
View = BlogPost
Title = CoreCLR on ARM
Time = 2015/07/14 01:05
```

Earlier today I got a C# hello world running on Ubuntu 14.04, on a Raspberry Pi 2. Sure this has been possible for a long time with Mono, the significance is that I was running the hello world with CoreCLR. This is the first time on Linux for ARM CoreCLR has worked, though a lot of the work was done thanks to Windows supporting ARM. This code is still yet to be merged, and it doesn't do much more, stack unwinding is broken which is needed for a lot of things, but it's a big step. I was very happy when I saw the words `Hello World` on my terminal.

![First hello world](https://s.paste.ninja/ben%40ubuntu-rpi_~gitcoreclrbinProductLinux.ar_2015-07-13_14-47-52.png)