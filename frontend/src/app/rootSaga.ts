import authSaga from "features/authenticate/authSaga"
import { cartSaga } from "features/cart/cartSaga"
import { productListSaga } from "features/ListProduct/listProductSaga"
import { all } from "redux-saga/effects"

export default function* rootSaga() {
    yield all([authSaga(), productListSaga(), cartSaga()])
}
