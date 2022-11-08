# BAPSI .NET
.NET implementation of
BAPSI (Bewator All Purpose Socket Interface) a socket API developed by Vanderbilt (former Bewator).

BAPSI is used for safe data transfer between external applications (e.g. time distribution and booking systems) and the Entro access control system.

Bewator Entro is a discontinued system, but the protocol and system is still interesting.

## Original BAPSI.DLL
Vanderbilt says:
> You can implement the interface to BAPSI in two ways, either do a full protocol implementation yourself or use the supplied DLL or OCX.

Using these proprietary binaries in modern applications is less then optimal. 

### Full protocol implementation
Vanderbilt continues with
> To get full control over the BAPSI implementation you can implement the protocol
including encryption, flow control and check sum handling.

This project is an attempt to re-implement this and sharing the source, which might also serve as documentation.
