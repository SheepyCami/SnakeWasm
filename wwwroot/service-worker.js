// Minimal service worker for Blazor WebAssembly
self.addEventListener('install', function(e) {
    self.skipWaiting();
});

self.addEventListener('activate', function(e) {
    e.waitUntil(self.clients.claim());
});

self.addEventListener('fetch', function(e) {
    // Don't handle non-GET requests
    if (e.request.method !== 'GET') {
        return;
    }

    e.respondWith(
        fetch(e.request)
            .catch(function() {
                // If the network request fails, return the offline page
                return new Response('Offline mode is not supported yet.');
            })
    );
});
