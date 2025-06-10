import AboutPage from "../pages/AboutPage";
import LogInPage from "../pages/LogInPage";
import NotFoundPage from "../pages/NotFoundPage";
import ShortURLsPage from "../pages/ShortURLsPage";

export const routesList = [
    {
        path: "*",
        element: <NotFoundPage />,
    },
    {
        path: "/",
        element: <ShortURLsPage />,
    },
    {
        path: "/about",
        element: <AboutPage />,
    },
    {
        path: "/login",
        element: <LogInPage />,
    }
];