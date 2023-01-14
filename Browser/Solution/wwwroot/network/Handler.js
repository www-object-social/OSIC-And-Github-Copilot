export function Handler(obj) {
	window.addEventListener("offline", () => obj.invokeMethod("JSChange", false));
	window.addEventListener("online", () => obj.invokeMethod("JSChange", true));
	return navigator.onLine;
}
