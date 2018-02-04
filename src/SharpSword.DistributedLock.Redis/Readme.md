分布式锁
http://www.cnblogs.com/ironPhoenix/p/6048467.html
https://github.com/kidfashion/redlock-cs

// Declare a Distributed Lock based on 3 REDIS servers

var dlm = new Redlock(
		ConnectionMultiplexer.Connect("127.0.0.1:6379"), 
		ConnectionMultiplexer.Connect("127.0.0.1:6380"), 
		ConnectionMultiplexer.Connect("127.0.0.1:6381"));

// Declare lock object.
Lock lockObject;

// Try to acquire the lock (with resourceName as lock identifier and an 
// expiration time of 10 seconds).
var locked = dlm.Lock(resourceName,	new TimeSpan(0, 0, 10), out lockObject);

// If locked is true, lockObject is populated and the lock has been acquired, 
// otherwise the lock has not been acquired.

// Tries to release the lock contained in lockObject.
dlm.Unlock(lockObject);