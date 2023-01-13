self.importScripts('./service-worker-assets.js');
self.addEventListener('install', event => event.waitUntil(onInstall(event)));
self.addEventListener('activate', event => event.waitUntil(onActivate(event)));
self.addEventListener('fetch', event => event.respondWith(onFetch(event)));
const cacheNamePrefix = 'offline-cache-';
const cacheName = `${cacheNamePrefix}${self.assetsManifest.version}`;
async function onInstall(event) { 
    await (await caches.open(cacheName)).addAll(self.assetsManifest.assets
        .filter(asset => asset.url.match(/\.(dll|pdb|wasm|html|js|json|css|woff|png|jpe?g|gif|ico|blat|dat)$/) && !asset.url.match(/^service-worker\.js$/))
        .map(asset => new Request(asset.url, { integrity: asset.hash, cache: 'no-cache' })));
}
async function onActivate(event) {
    await Promise.all((await caches.keys())
        .filter(key => key.startsWith(cacheNamePrefix) && key !== cacheName)
        .map(key => caches.delete(key)));
}
async function onFetch(event) {
    if (event.request.method === 'GET') {
        return (await (await caches.open(cacheName)).match(event.request.mode === 'navigate' ? 'index.html' : event.request)) || fetch(event.request);
    }
}