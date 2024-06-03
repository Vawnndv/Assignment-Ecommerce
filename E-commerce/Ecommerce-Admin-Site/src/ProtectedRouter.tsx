import { useSelector } from "react-redux";
import { AppDispatch, RootState, useAppDispatch } from "./redux/store";
import { Navigate, Outlet } from "react-router-dom";
import { logoutAction } from "./redux/actions/authActions";

function ProtectedRouter() {

    const MyDispatch: AppDispatch = useAppDispatch();
    const { userInfo } = useSelector((state: RootState) => state.userLogin);
    return userInfo?.token ? (
        userInfo?.role === "Admin" ? <Outlet /> : (
            MyDispatch(logoutAction())
        )
    ) : <Navigate to="/Login" />
}

export { ProtectedRouter }
