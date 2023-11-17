module.exports = {
    content: ["./Pages/**/*.{html,js}"],
    //purge: [],
    darkMode: false, // or 'media' or 'class'
    theme: {
        screens: {
            sm: '480px',
            md: '768px',
            lg: '976px',
            xl: '1440px',
        },
        extend: {
            zIndex: {
                '9998': '9998'
            },
            width: {
                '300': '300px'
            },
        },
        minHeight: {
            '1/2': '50%',
        },
        //        height: {
        //            fit: 'fit-content',
        //            '7em': '1.75em'
        //        },
        //        width: {
        //            fit: 'fit-content',
        //            '7em': '1.75em'
        //        },

        colors: {
            coolGray: {
                50: "#F9FAFB",
                100: "#F3F4F6",
                150: "#EBECF0",
                200: "#E5E7EB",
                300: "#D1D5DB",
                400: "#9CA3AF",
                500: "#6B7280",
                600: "#4B5563",
                700: "#374151",
                800: "#1F2937",
                900: "#111827",
            },
            blueGray: {
                50: "#F8FAFC",
                100: "#F1F5F9",
                200: "#E2E8F0",
                300: "#CBD5E1",
                400: "#94A3B8",
                500: "#64748B",
                600: "#475569",
                700: "#334155",
                800: "#1E293B",
                900: "#0F172A",
            },
            customPurple: {
                10: "#312e5a",
                50: "#312e81"
            },
            background: {
                1: "#29292f",
                2: "#1d174a",
                3: "#221887"
            },
            goldenText: {
                1: "#db8e12"
            },
            border: {
                1: "#ffa07a"
            }
        }
    },
    variants: {
        extend: {
            backgroundColor: ['odd'],
            display: ['group-hover', 'group-focus', 'focus-within', 'hover'],
            textColor: ['group-focus'],
        },
    },
    plugins: [
        require("@tailwindcss/forms")({
            strategy: 'class',
        })
    ],
}
