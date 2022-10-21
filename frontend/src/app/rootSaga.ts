import authSaga from "features/authenticate/authSaga"
import { all } from "redux-saga/effects"

export default function* rootSaga() {
    yield all([authSaga()])
}
