import createSagaMiddleware from "@redux-saga/core"
import { Action, combineReducers, configureStore, ThunkAction } from "@reduxjs/toolkit"
import AuthReducer from "features/authenticate/authSlice"
import cartReducer from "features/cart/cartSlice"
import productReducer from "features/ListProduct/listProductSlice"
import {
    FLUSH,
    PAUSE,
    PERSIST,
    persistReducer,
    persistStore,
    PURGE,
    REGISTER,
    REHYDRATE,
} from "redux-persist"
import storage from "redux-persist/lib/storage"

import rootSaga from "./rootSaga"

const rootReducer = combineReducers({
    auth: AuthReducer,
    product: productReducer,
    cart: cartReducer,
})

const persistConfig = {
    key: "root",
    storage,
    blacklist: ["product"],
}

const persistedReducer = persistReducer(persistConfig, rootReducer)

const sagaMiddleware = createSagaMiddleware()
export const store = configureStore({
    reducer: persistedReducer,
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware({
            serializableCheck: {
                ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER],
            },
        }).concat(sagaMiddleware),
})
export let persistor = persistStore(store)
sagaMiddleware.run(rootSaga)
export type AppDispatch = typeof store.dispatch
export type RootState = ReturnType<typeof store.getState>
export type AppThunk<ReturnType = void> = ThunkAction<
    ReturnType,
    RootState,
    unknown,
    Action<string>
>
