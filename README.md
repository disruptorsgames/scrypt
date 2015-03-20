# Scrypt
**scrypt** is a basic encoding / decoding utility namely written for use encoding 
and decoding [Base64](https://en.wikipedia.org/wiki/Base64) strings like the following:

```
VGhpcyBpcyBhIEJhc2U2NCBzdHJpbmcgYnVpbHQgdXNpbmcgdGhpcyB0b29sLg==
```

# Examples
To [e]ncode and [d]ecode use the following:
```
scrypt /e "Hello World"
scrypt /d SGVsbG8gV29ybGQh
```

To [h]ash use the following, the default is sha1
```
scrypt /e "Hello World" /h md5
```