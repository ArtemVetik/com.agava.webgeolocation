const library = {

    // Class definition.

    $geolocationSdk: {
        isSupported: function () {
            return 'geolocation' in navigator;
        },

        throwIfGeolocationNotSupported: function () {
            if (!geolocationSdk.isSupported()) {
                throw new Error('Geolocation is not supported in this browser.');
            }
        },

        getCurrentPosition: function (successCallbackPtr, errorCallbackPtr, options) {
            geolocationSdk.throwIfGeolocationNotSupported();

            navigator.geolocation.getCurrentPosition(
                function (position) {
                    const positionObject = geolocationSdk.createPositionObject(position, 0);
                    geolocationSdk.jsonCallback(positionObject, successCallbackPtr);
                },
                function (error) {
                    const errorObject = geolocationSdk.createErrorObject(error);
                    geolocationSdk.jsonCallback(errorObject, errorCallbackPtr);
                },
                JSON.parse(UTF8ToString(options))
            );
        },

        watchPosition: function (successCallbackPtr, errorCallbackPtr, options) {
            geolocationSdk.throwIfGeolocationNotSupported();

            const watchId = navigator.geolocation.watchPosition(
                function (position) {
                    const positionObject = geolocationSdk.createPositionObject(position, watchId);
                    geolocationSdk.jsonCallback(positionObject, successCallbackPtr);
                },
                function (error) {
                    const errorObject = geolocationSdk.createErrorObject(error);
                    geolocationSdk.jsonCallback(errorObject, errorCallbackPtr);
                },
                JSON.parse(UTF8ToString(options))
            );
            return watchId;
        },

        clearWatch: function (watchId) {
            geolocationSdk.throwIfGeolocationNotSupported();

            navigator.geolocation.clearWatch(watchId);
        },

        createPositionObject: function (position, watchId) {
            return {
                watchId: watchId,
                coords: {
                    accuracy: position.coords.accuracy,
                    altitude: position.coords.altitude,
                    altitudeAccuracy: position.coords.altitudeAccuracy,
                    heading: position.coords.heading,
                    latitude: position.coords.latitude,
                    longitude: position.coords.longitude,
                    speed: position.coords.speed,
                },
                timestamp: position.timestamp,
            };
        },

        createErrorObject: function (error) {
            return {
                code: error.code,
                message: error.message,
            };
        },

        jsonCallback: function (result, callbackPtr) {
            const entriesJson = JSON.stringify(result);
            const stringBufferSize = lengthBytesUTF8(entriesJson) + 1;
            const stringBufferPtr = _malloc(stringBufferSize);
            stringToUTF8(entriesJson, stringBufferPtr, stringBufferSize);
            dynCall('vi', callbackPtr, [stringBufferPtr]);
            _free(stringBufferPtr);
        },
    },

    // External C# calls.

    IsSupported: function () {
        return geolocationSdk.isSupported();
    },

    GetCurrentPosition: function (successCallback, errorCallback, options) {
        geolocationSdk.getCurrentPosition(successCallback, errorCallback, options);
    },

    WatchPosition: function (successCallback, errorCallback, options) {
        return geolocationSdk.watchPosition(successCallback, errorCallback, options);
    },

    ClearWatch: function (watchId) {
        geolocationSdk.clearWatch(watchId);
    },
}

autoAddDeps(library, '$geolocationSdk');
mergeInto(LibraryManager.library, library);